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

        public List<User> GetAll()
        {
            return _users.Find(_ => true).ToList();
        }

        public User GetById(string id)
        {
            return _users.Find(user => user.Id == id).FirstOrDefault();
        }

        public void Create(User user)
        {
            user.Id=Guid.NewGuid().ToString();
            _users.InsertOne(user);
        }


        public void Delete(string id)
        {
            _users.DeleteOne(user => user.Id == id);
        }
    }
}
