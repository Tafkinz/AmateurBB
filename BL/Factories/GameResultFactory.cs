using System;
using System.Collections.Generic;
using System.Text;
using BL.DTO;
using Model;

namespace BL.Factories
{
    public class GameResultFactory : IGameResultFactory
    {
        public GameResultActionDto GetGameResultActionDto(GameTeam homeTeam, GameTeam awayTeam, ApplicationUser referee, CourtDTO court)
        {
            var gameResult = GetGameResultsByTeam(homeTeam, awayTeam, referee, court);
            GameResultActionDto dto = new GameResultActionDto()
            {
                GameResult = gameResult,
                Accept = false,
                GameTs = homeTeam.Game.GameTs
            };
            return dto;
        }

        public GameResultDTO GetGameResultsByTeam(GameTeam homeTeam, GameTeam awayTeam, ApplicationUser referee, CourtDTO court)
        {
            return new GameResultDTO()
            {
                HomeTeam = homeTeam.Team.TeamName,
                AwayTeam = awayTeam.Team.TeamName,
                AwayTeamPoints = awayTeam.Points,
                HomeTeamPoints = homeTeam.Points,
                Court = court,
                Referee = referee.DisplayName,
                GameResultId = homeTeam.GameResultId
            };
        }
    }
}
