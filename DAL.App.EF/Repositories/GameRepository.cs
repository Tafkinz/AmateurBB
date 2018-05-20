using System;
using System.Collections.Generic;
using System.Text;
using DAL.App.Interfaces.Repositories;
using DAL.EF.Repositories;
using Microsoft.EntityFrameworkCore;
using Model;

namespace DAL.App.EF.Repositories
{
    public class GameRepository : EFRepository<Game>, IGameRepository
    {
        public GameRepository(DbContext dbContext) : base(dbContext)
        {
        }
    }
}
