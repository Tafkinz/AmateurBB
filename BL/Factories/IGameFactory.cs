﻿using System;
using System.Collections.Generic;
using System.Text;
using BL.DTO;
using Model;

namespace BL.Factories
{
    public interface IGameFactory
    {
        Game Create(GameDTO game);
        GameDTO Create(Game game, CourtDTO court, ApplicationUser referee, GameType gameType);
    }
}
