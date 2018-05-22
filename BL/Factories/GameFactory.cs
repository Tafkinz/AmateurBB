using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BL.DTO;
using Model;

namespace BL.Factories
{
    public class GameFactory : IGameFactory
    {
        public Game Create(GameDTO game)
        {
            return new Game()
            {
                GameTypeId =  game.GameType.GameTypeId,
                ApplicationUserId = game.RefereeId,
                CourtId = game.Court.CourtId,
                GameTs = game.GameTs
            };
        }

        public GameDTO Create(Game game, CourtDTO court, ApplicationUser referee, GameType gameType)
        {
            return new GameDTO()
            {
                HomeTeamId = game.GameTeams[0].TeamId,
                HomeTeamName = game.GameTeams[0].Team.FullTeamName,
                HomeTeamPoints = game.GameTeams[0].Points,
                Court = court,
                GameId = game.GameId,
                GameTs = game.GameTs,
                GameType = gameType,
                AwayTeamId = game.GameTeams[1].TeamId,
                AwayTeamPoints = game.GameTeams[1].Points,
                AwayTeamName = game.GameTeams[1].Team.FullTeamName,
                Referee = referee.DisplayName,
                RefereeId = referee.Id

            };
        }
    }
}
