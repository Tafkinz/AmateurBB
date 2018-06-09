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
    public class ContactRepository : EFRepository<Contact>, IContactRepository
    {
        public ContactRepository(ApplicationDbContext repositoryContext) : base(repositoryContext)
        {
        }

        public List<Contact> GetByUserId(string userId)
        {
            return RepositoryDbSet.Include(p => p.ContactType)
                .Where(c => c.ApplicationUserId == userId).ToList();
        }

        public Contact GetContact(long id)
        {
            return RepositoryDbSet.Include(p => p.ContactType)
                .Where(c => c.ContactId == id).SingleOrDefault();
        }
    }
} 
