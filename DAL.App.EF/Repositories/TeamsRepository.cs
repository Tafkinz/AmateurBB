using System;
using System.Collections.Generic;
using System.Text;
using DAL.App.Interfaces.Repositories;
using DAL.EF.Repositories;
using Model;

namespace DAL.App.EF.Repositories
{
    public class TeamsRepository : EFRepository<Team>, ITeamRepository
    {
        public TeamsRepository(ApplicationDbContext repositoryContext) : base(repositoryContext)
        {
        }
    }
}
