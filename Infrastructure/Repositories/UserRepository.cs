using Domain.Entities;
using Domain.IRepositories;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class UserRepository :BaseMongoRepository, IUserRepository
    {
        private readonly IMongoCollection<User> _users;
        
        public UserRepository(IMongoDbSettings mongoDbSettings) : base(mongoDbSettings)
        {
            _users = MongoDatabase.GetCollection<User>(mongoDbSettings.UserCollectionName);
        }
        
        public async Task Add(User user)
        {
            await _users.InsertOneAsync(user);
        }

        public Task<List<User>> GetAll()
        {
            var result =  _users.Find(_ => true).ToList();
            return Task.FromResult(result) ;
        }

        public Task<User> Authenticate(string email, string password)
        {
            var user = _users.Find(u => u.Email == email && u.Password == password).FirstOrDefault();

            return Task.FromResult(user);
        }

        public Task<User> GetUserByEmail(string email)
        {
            var result = _users.Find(u => u.Email == email).FirstOrDefault();
            
           return Task.FromResult(result);

        }
    }
}
