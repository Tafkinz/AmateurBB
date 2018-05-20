using System;
using System.Collections.Generic;
using System.Text;
using BL.DTO;
using Model;

namespace BL.Factories
{
    public interface IGameResultFactory
    {
        GameResultDTO GetGameResultsByTeam(GameTeam homeTeam, GameTeam awayTeam, ApplicationUser referee, CourtDTO court);

        GameResultActionDto GetGameResultActionDto(GameTeam homeTeam, GameTeam awayTeam, ApplicationUser referee,
            CourtDTO court);
    }
}
