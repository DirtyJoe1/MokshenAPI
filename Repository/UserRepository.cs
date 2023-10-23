using Entities;
using Entities.Models;
using Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class UserRepository : RepositoryBase<User>, IUserRepository
    {
        public UserRepository(RepositoryContext repositoryContext) : base(repositoryContext) { }
        public async Task<List<User>> GetUsersAsync(Guid id, bool trackChanges)
        {
            var users = await FindByCondition(e => e.Id.Equals(id), trackChanges).ToListAsync();
            return users;
        }
        public async Task<User> GetUserAsync(Guid id, bool trackChanges) => await FindByCondition(e => e.Id.Equals(id), trackChanges).SingleOrDefaultAsync();
        public void CreateUser(User user)
        {
            Create(user);
        }
        public void DeleteUser(User user)
        {
            Delete(user);
        }
    }
}
