using System;
using System.Collections.Generic;
using System.Text;
using BL.DTO;
using Model;

namespace BL.Factories
{
    public interface IGameResultFactory
    {
        GameDTO GetGameResultsByTeam(GameTeam homeTeam, GameTeam awayTeam, ApplicationUser referee, CourtDTO court);

        GameActionDto GetGameResultActionDto(GameTeam homeTeam, GameTeam awayTeam, ApplicationUser referee,
            CourtDTO court);
    }
}
