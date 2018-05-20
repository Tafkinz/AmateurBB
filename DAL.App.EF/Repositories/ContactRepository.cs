using System;
using System.Collections.Generic;
using System.Text;
using DAL.App.Interfaces.Repositories;
using DAL.EF.Repositories;
using Model;

namespace DAL.App.EF.Repositories
{
    public class ContactRepository : EFRepository<Contact>, IContactRepository
    {
        public ContactRepository(ApplicationDbContext repositoryContext) : base(repositoryContext)
        {
        }
    }
}
