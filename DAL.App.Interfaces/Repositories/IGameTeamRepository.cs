﻿using System;
using System.Collections.Generic;
using System.Text;
using DAL.interfaces.Repositories;
using Model;

namespace DAL.App.Interfaces.Repositories
{
    public interface IGameTeamRepository : IRepository<GameTeam>
    {
        List<GameTeam> GetGameTeamsByGameId(long gameId);
    }
}
