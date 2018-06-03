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
    public class GameRepository : EFRepository<Game>, IGameRepository
    {
        private readonly DbContext _dbContext;
        public GameRepository(DbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public Game GetGameById(long id)
        {
            return RepositoryDbSet
                .Include(p => p.Court)
                .Include(p => p.GameTeams)
                .Include(p => p.Referee)
                .Include(p => p.GameType)
                .SingleOrDefault(p => p.GameId == id);
        }
    }
}
