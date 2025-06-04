using FitTrack.DL.Interfaces;
using FitTrack.Models.Configurations;
using FitTrack.Models.DTO;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace FitTrack.DL.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IMongoCollection<User> _users;

        public UserRepository(IOptionsMonitor<MongoDbConfiguration> mongoConfig)
        {
            var client = new MongoClient(mongoConfig.CurrentValue.ConnectionString);
            var database = client.GetDatabase(mongoConfig.CurrentValue.DatabaseName);
            _users = database.GetCollection<User>($"{nameof(User)}s");
        }

        public async Task<List<User>> GetAllAsync()
        {
            var result = await _users.FindAsync(_ => true);
            return await result.ToListAsync();
        }

        public async Task<User?> GetByIdAsync(string id)
        {
            var result = await _users.FindAsync(user => user.Id == id);
            return await result.FirstOrDefaultAsync();
        }

        public async Task CreateAsync(User user)
        {
            user.Id = Guid.NewGuid().ToString();
            await _users.InsertOneAsync(user);
        }

        public async Task DeleteAsync(string id)
        {
            await _users.DeleteOneAsync(user => user.Id == id);

        }
        public async Task<IEnumerable<User?>> FullLoad()
        {
            Console.WriteLine("FullLoad called for UserRepository");
            return await GetAllAsync();
        }

        public async Task<IEnumerable<User?>> DifLoad(DateTime lastExecuted)
        {
            Console.WriteLine($"DifLoad called for UserRepository since {lastExecuted}");
            var filter = Builders<User>.Filter.Gt(u => u.UpdatedAt, lastExecuted);
            var result = await _users.FindAsync(filter);
            return await result.ToListAsync();
        }
    }

}
