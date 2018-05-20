using System;
using System.Collections.Generic;
using System.Text;
using DAL.App.Interfaces.Repositories;
using DAL.EF.Repositories;
using Microsoft.EntityFrameworkCore;
using Model;

namespace DAL.App.EF.Repositories
{
    public class ContactTypeRepository : EFRepository<ContactType>, IContactTypeRepository
    {
        public ContactTypeRepository(DbContext dbContext) : base(dbContext)
        {
        }
    }
}
