using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using DAL.interfaces.Repositories;
using Model;

namespace DAL.App.Interfaces.Repositories
{
    public interface ITeamRepository : IRepository<Team>
    {
        List<Team> GetAll();

        Team Find(params object[] keyValues);
    }
}
