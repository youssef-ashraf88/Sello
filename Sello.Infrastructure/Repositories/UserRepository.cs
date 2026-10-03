using Microsoft.EntityFrameworkCore;
using Sello.Domain.Entities.Identity;
using Sello.Domain.RepositoryContracts;
using Sello.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sello.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _db;

        public UserRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public IQueryable<ApplicationUser> GetAllUsers()
        {
            return _db.Users.AsNoTracking();
        }

        public async Task<int> GetTotalUsers()
        {
            return await _db.Users.CountAsync();
        }
    }
}
