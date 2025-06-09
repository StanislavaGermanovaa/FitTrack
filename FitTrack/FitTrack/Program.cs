using FitTrack.ServiceExtensions; 
using Mapster;
using FitTrack.BL;
using FitTrack.DL;
using Serilog.Sinks.SystemConsole.Themes;
using Serilog;
using FitTrack.HealthCheck;
using FluentValidation;
using FluentValidation.AspNetCore;
using FitTrack.Validators;
using FitTrack.DL.Gateways;


var builder = WebApplication.CreateBuilder(args);

var logger = new LoggerConfiguration()
    .Enrich.FromLogContext()
    .WriteTo.Console(theme:
        AnsiConsoleTheme.Code)
    .CreateLogger();

builder.Logging.AddSerilog(logger);

// Add services to the container.
builder.Services
    .AddConfigurations(builder.Configuration)
    .AddDataDependencies(builder.Configuration)
    .AddBusinessDependencies();

builder.Services.AddMapster();

builder.Services.AddValidatorsFromAssemblyContaining<UserRequestValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<SubscriptionRequestValidator>();
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddScoped<IUserDetailsGateway, UserDetailsGateway>();


builder.Services.AddControllers();
builder.Services.AddSwaggerGen();

builder.Services.AddHealthChecks()
    .AddCheck<CustomHealthCheck>("Sample");

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapHealthChecks("/healthz");


// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();