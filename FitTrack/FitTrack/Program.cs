using FitTrack.ServiceExtensions; 
using Mapster;
using FitTrack.BL;
using FitTrack.DL;
using Serilog.Sinks.SystemConsole.Themes;
using Serilog;


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
    .AddDataDependencies()
    .AddBusinessDependencies();

builder.Services.AddMapster();

//builder.Services.AddValidatorsFromAssemblyContaining<TestRequest>();
//builder.Services.AddFluentValidationAutoValidation();

builder.Services.AddControllers();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();