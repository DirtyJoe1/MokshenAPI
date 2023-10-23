using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces
{
    public interface IUserRepository
    {
        Task<List<User>> GetUsersAsync(Guid id, bool trackChanges);
        Task<User> GetUserAsync(Guid id, bool trackChanges);
        void CreateUser (User user);
        void DeleteUser(User user);
    }
}
