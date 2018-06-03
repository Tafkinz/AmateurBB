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
    public class PersonTypeRepository : EFRepository<PersonType>, IPersonTypeRepository
    {
        public PersonTypeRepository(DbContext dbContext) : base(dbContext)
        {
        }

        public bool Exists(string personTypeName)
        {
            return RepositoryDbSet.Any(p => p.PersonTypeName == personTypeName);
        }
    }
}
