using System;
using System.Collections.Generic;
using System.Text;
using DAL.interfaces.Repositories;
using Model;

namespace DAL.App.Interfaces.Repositories
{
    public interface IContactRepository : IRepository<Contact>
    {
        Contact GetContact(long id);

        List<Contact> GetByUserId(string userId);
    }
}
