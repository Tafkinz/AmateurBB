using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAL.App.Interfaces.Repositories;
using DAL.EF;
using DAL.EF.Repositories;
using Microsoft.EntityFrameworkCore;
using Model;

namespace DAL.App.EF.Repositories
{
    class StandingsRepository : EFRepository<Standings>, IStandingsRepository
    {
        public StandingsRepository(ApplicationDbContext repositoryContext) : base(repositoryContext)
        {
        }

        public List<Standings> GetAllStandings()
        {
            return RepositoryDbSet
                .Include(p => p.Team)
                .ToList();
        }

        public Standings GetByTeamId(long teamId)
        {
            return RepositoryDbSet
                .Include(p => p.Team)
                .Where(s => s.TeamId == teamId)
                .FirstOrDefault();
        }
    }
}
