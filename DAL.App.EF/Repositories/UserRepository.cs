using System;
using System.Collections.Generic;
using System.Text;
using DAL.App.Interfaces.Repositories;
using DAL.EF.Repositories;
using Microsoft.EntityFrameworkCore;
using Model;

namespace DAL.App.EF.Repositories
{
    public class UserRepository : EFRepository<ApplicationUser>, IUserRepository
    {
        public ApplicationUser GetAllUsers()
        {
            throw new NotImplementedException();
        }

        public UserRepository(DbContext dbContext) : base(dbContext)
        {
        }
    }
}
