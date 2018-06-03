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
    public class TeamsRepository : EFRepository<Team>, ITeamRepository
    {
        public TeamsRepository(ApplicationDbContext repositoryContext) : base(repositoryContext)
        {

        }

        public List<Team> GetAll()
        {
            return RepositoryDbSet
                .Include(c => c.TeamPersons)
                .Include(c => c.GameTeams)
                .ToList();
        }

        public Team Find(params object[] keyValues)
        {
            return RepositoryDbSet.Include(p => p.GameTeams).Include(p => p.TeamPersons)
                .FirstOrDefault(p => p.TeamId == (long)keyValues[0]);
        }
    }
}
