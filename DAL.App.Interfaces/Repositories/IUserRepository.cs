using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using DAL.interfaces.Repositories;
using Model;

namespace DAL.App.Interfaces.Repositories
{
    public interface IUserRepository : IRepository<ApplicationUser>
    {
        List<ApplicationUser> GetAllUsers();

        ApplicationUser FindById(string id);

        ApplicationUser FindByEmail(string email);
    }
}
