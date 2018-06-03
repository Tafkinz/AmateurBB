using System;
using System.Collections.Generic;
using System.Text;
using BL.DTO;
using Model;

namespace BL.Factories
{
    public class GameResultFactory : IGameResultFactory
    {
        public GameDTO GetGameResultsByTeam(GameTeam homeTeam, GameTeam awayTeam, ApplicationUser referee, CourtDTO court)
        {
            GameDTO gameDTO = new GameDTO()
            {
                AwayTeamId = awayTeam.TeamId,
                AwayTeamName = awayTeam.Team.TeamName,
                AwayTeamPoints = awayTeam.Points,
                HomeTeamId = homeTeam.TeamId,
                HomeTeamName = homeTeam.Team.TeamName,
                HomeTeamPoints = homeTeam.Points,
                Referee = referee.DisplayName,
                RefereeId = referee.Id,
                Court = court,
                GameId = homeTeam.GameId
            };

            return gameDTO;
        }
    }
}
 