using System;
using System.Collections.Generic;
using System.Text;
using DAL.App.Interfaces.Repositories;
using DAL.EF.Repositories;
using Microsoft.EntityFrameworkCore;
using Model;

namespace DAL.App.EF.Repositories
{
    public class GameResultRepository : EFRepository<GameResult>, IGameResultRepository
    {
        public GameResultRepository(DbContext dbContext) : base(dbContext)
        {
        }
    }
}
