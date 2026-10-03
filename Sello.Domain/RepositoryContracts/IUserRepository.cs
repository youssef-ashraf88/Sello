using Sello.Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sello.Domain.RepositoryContracts
{
    public interface IUserRepository
    {
        IQueryable<ApplicationUser> GetAllUsers();
        Task<int> GetTotalUsers();
    }
}
