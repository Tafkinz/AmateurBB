using System;
using System.Collections.Generic;
using System.Text;
using DAL.interfaces.Repositories;
using Model;

namespace DAL.App.Interfaces.Repositories
{
    public interface IUserRepository : IRepository<ApplicationUser>
    {
        ApplicationUser GetAllUsers();
    }
}
