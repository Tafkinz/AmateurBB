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
    public class GameTypeRepository : EFRepository<GameType>, IGameTypeRepository
    {
        public GameTypeRepository(DbContext dbContext) : base(dbContext)
        {
        }

        public bool Exists(string gameTypeName)
        {
            return RepositoryDbSet.Any(p => p.GameTypeName == gameTypeName);
        }
    }
}
