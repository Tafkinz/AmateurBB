using System;
using System.Collections.Generic;
using System.Text;
using DAL.App.Interfaces.Repositories;
using DAL.EF.Repositories;
using Microsoft.EntityFrameworkCore;
using Model;

namespace DAL.App.EF.Repositories
{
    public class GameTeamRepository : EFRepository<GameTeam>, IGameTeamRepository
    {
        public GameTeamRepository(DbContext dbContext) : base(dbContext)
        {
        }
    }
}
