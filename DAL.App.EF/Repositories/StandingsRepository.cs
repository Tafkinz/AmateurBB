using System;
using System.Collections.Generic;
using System.Text;
using DAL.App.Interfaces.Repositories;
using DAL.EF;
using DAL.EF.Repositories;
using Model;

namespace DAL.App.EF.Repositories
{
    class StandingsRepository : EFRepository<Standings>, IStandingsRepository
    {
        public StandingsRepository(ApplicationDbContext repositoryContext) : base(repositoryContext)
        {
        }
    }
}
