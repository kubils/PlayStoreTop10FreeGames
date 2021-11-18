using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Entities;

namespace Domain.IRepositories
{
    public interface IUserRepository
    {
        Task<User> Add(User user);

        Task<List<User>> GetAll();
        Task<User> Authenticate(string email, string password);
        Task<User> GetUserByEmail(string email);
    }
}