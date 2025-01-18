using FitTrack.DL.Interfaces;
using FitTrack.Models.DTO;
using MongoDB.Driver;

namespace FitTrack.DL.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IMongoCollection<User> _users;

        public UserRepository(IMongoDatabase database)
        {
            _users = database.GetCollection<User>("Users");
        }

        public IEnumerable<User> GetAll()
        {
            return _users.Find(_ => true).ToList();
        }

        public User GetById(string id)
        {
            return _users.Find(user => user.Id == id).FirstOrDefault();
        }

        public void Create(User entity)
        {
            _users.InsertOne(entity);
        }

        public void Update(string id, User entity)
        {
            _users.ReplaceOne(user => user.Id == id, entity);
        }

        public void Delete(string id)
        {
            _users.DeleteOne(user => user.Id == id);
        }
    }
    public interface ISubscriptionRepository
    {
        IEnumerable<Subscription> GetAll();
        Subscription GetById(string id);
        void Create(Subscription entity);
        void Update(string id, Subscription entity);
        void Delete(string id);
    }
}
