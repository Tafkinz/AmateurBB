using System;
using System.Collections.Generic;
using System.Text;
using DAL.interfaces.Repositories;
using Model;

namespace DAL.App.Interfaces.Repositories
{
    public interface IStandingsRepository : IRepository<Standings>
    {
        List<Standings> GetAllStandings();

        Standings GetByTeamId(long teamId);
    }
}
