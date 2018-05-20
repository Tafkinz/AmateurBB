using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.App.Interfaces.Repositories;
using DAL.EF.Repositories;
using Microsoft.EntityFrameworkCore;
using Model;

namespace DAL.App.EF.Repositories
{
    public class CourtRepository : EFRepository<Court>, ICourtRepository
    {
        private DbContext _context;
        public CourtRepository(DbContext dbContext) : base(dbContext)
        {
            _context = dbContext;
        }

        public Court GetCourtById(long courtId)
        {
            return RepositoryDbSet.SingleOrDefault(c => c.CourtId == courtId);
        }
    }
}
