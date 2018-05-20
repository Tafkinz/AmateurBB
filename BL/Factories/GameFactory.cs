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
                ApplicationUserId = game.Referee.UserId,
                CourtId = game.Court.CourtId,
                GameTs = game.GameTs
            };
        }

        public GameDTO Create(Game game, CourtDTO court, UserDTO referee, GameType gameType)
        {
            return new GameDTO()
            {
                AwayTeamId = game.GameTeams.Where(p => !p.IsHomeTeam).Single().TeamId,
                Court = court,
                GameId = game.GameId,
                GameTs = game.GameTs,
                GameType = gameType,
                HomeTeamId = game.GameTeams.Where(p => p.IsHomeTeam).Single().TeamId,
                Referee = referee
            };
        }
    }
}
