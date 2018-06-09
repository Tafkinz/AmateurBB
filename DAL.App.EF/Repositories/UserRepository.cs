using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAL.App.Interfaces.Repositories;
using DAL.EF.Repositories;
using Microsoft.EntityFrameworkCore;
using Model;

namespace DAL.App.EF.Repositories
{
    public class UserRepository : EFRepository<ApplicationUser>, IUserRepository
    {

        public UserRepository(DbContext dbContext) : base(dbContext)
        {
        }

        public ApplicationUser FindByEmail(string email)
        {
            return RepositoryDbSet.Where(p => p.Email == email)
                .Include(p => p.PersonType)
                .Include(p => p.Contacts)
                .First();
        }

        public ApplicationUser FindById(string id)
        {
            return RepositoryDbSet
                .Include(p => p.PersonType)
                .Include(p => p.Contacts)
                .Include(p => p.TeamPersons)
                .Where(p => p.Id == id)
                .First();
        }

        public List<ApplicationUser> GetAllUsers()
        {
            return RepositoryDbSet
                .Include(p => p.TeamPersons)
                .Include(p => p.PersonType)
                .ToList();
        }
    }
}
