using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using BL.DTO;
using BL.Factories;
using BL.Util;
using DAL.App.Interfaces;
using DAL.App.Interfaces.Repositories;
using DAL.Interfaces.Helpers;
using Microsoft.AspNetCore.Identity;
using Model;

namespace BL.Services
{
    public class GameService : IGameService
    {
        private readonly IAppUnitOfWork _uow;
        private readonly IStandingFactory _standingFactory;
        private readonly IGameFactory _gameFactory;
        private readonly ICourtFactory _courtFactory;
        private readonly IUserFactory _userFactory;
        private readonly IGameResultFactory _gameResultFactory;
        private readonly AuthUtil _auth;

        public GameService(IAppUnitOfWork uow, IStandingFactory factory, IGameFactory gameFactory, ICourtFactory courtFactory, IUserFactory userFactory, AuthUtil auth, IGameResultFactory gameResultFactory)
        {
            _uow = uow;
            _standingFactory = factory;
            _gameFactory = gameFactory;
            _courtFactory = courtFactory;
            _userFactory = userFactory;
            _auth = auth;
            _gameResultFactory = gameResultFactory;
        }

        public async Task<List<StandingDTO>> GetAllStandingsAsync()
        {
            List<Standings> standingsList =  _uow.GetCustomRepository<IStandingsRepository>().GetAllStandings();
            List<StandingDTO> result = new List<StandingDTO>();
            standingsList.ForEach(p => result.Add(_standingFactory.Create(p)));
            return result;
        }

        public List<StandingDTO> GetAllStandings()
        {
            List<Standings> standingsList = _uow.GetCustomRepository<IStandingsRepository>().GetAllStandings();
            List<StandingDTO> result = new List<StandingDTO>();
            standingsList.ForEach(p => result.Add(_standingFactory.Create(p)));
            return result;
        }

        public StandingDTO GetStandingsByTeamId(long teamId)
        {
            Standings standing = _uow.GetCustomRepository<IStandingsRepository>().GetByTeamId(teamId);
            return _standingFactory.Create(standing);
        }

        public async Task<GameDTO> CreateGame(GameDTO gameDto)
        {
            Game game = _gameFactory.Create(gameDto);
            game = _uow.Games.Add(game);

            GameTeam gameTeam1 = BuildGameTeam(game, gameDto.AwayTeamId);
            GameTeam gameTeam2 = BuildGameTeam(game, gameDto.HomeTeamId);

            await _uow.GameTeams.AddAsync(gameTeam1);
            await _uow.GameTeams.AddAsync(gameTeam2);

            Team team1 = _uow.Teams.Find(gameDto.AwayTeamId);
            Team team2 = _uow.Teams.Find(gameDto.HomeTeamId);
            game = _uow.GetCustomRepository<IGameRepository>().GetGameById(game.GameId);

            game.GameTeams.Where(p => p.TeamId == team1.TeamId).Single().Team = team1;
            game.GameTeams.Where(p => p.TeamId == team2.TeamId).Single().Team = team2;
            _uow.SaveChanges();
            return _gameFactory.Create(game, _courtFactory.Create(game.Court));
        }

        public async Task<List<GameDTO>> GetUserPendingResults()
        {
            List<GameTeam> unapprovedGames = new List<GameTeam>();
            List<GameDTO> gameResults = new List<GameDTO>();
            string userId = _auth.GetCurrentUserId();
            ApplicationUser user = _uow.GetCustomRepository<IUserRepository>().FindById(userId);
            if (user.PersonType.PersonTypeName == Model.Enum.PersonTypes.Manager)
            {
                Team team = _uow.TeamPersons.GetAll().Single(p => p.ApplicationUserId == user.Id).Team;
                if (team == null) return null;
                unapprovedGames = _uow.GameTeams.GetAll().Where(p => p.TeamId == team.TeamId).Where(p => p.ResultConfirmed != true).ToList();
            }
            else if (user.PersonType.PersonTypeName == Model.Enum.PersonTypes.Referee)
            {
                List<Game> refGames = _uow.GetCustomRepository<IGameRepository>().GetAllGames().Where(p => p.ApplicationUserId == user.Id && p.RefereeConfirmed != true && p.ConfirmedTs == null).ToList();
                refGames.ForEach(p => unapprovedGames.Add(p.GameTeams[0]));
            }
            else
            {
                return gameResults;
            }

            foreach (GameTeam game in unapprovedGames)
            {
                GameTeam opposingTeam = game.Game.GameTeams.Single(p => !p.Equals(game));
                var referee = game.Game.Referee;
                var court = game.Game.Court;
                gameResults.Add(_gameResultFactory.GetGameResultsByTeam(game, opposingTeam, referee, _courtFactory.Create(court)));
            }

            return gameResults;
        }

        public async Task<GameDTO> AcceptGame(long id, GameAcceptDTO gameAccept)
        {
            string userId = _auth.GetCurrentUserId();
            ApplicationUser user = _uow.GetCustomRepository<IUserRepository>().FindById(userId);

            Game game = _uow.GetCustomRepository<IGameRepository>().GetGameById(id);
            if (game == null) return null;
            if (!game.GameTeams.Any(p => _uow.GetCustomRepository<ITeamRepository>().Find(p.TeamId).TeamPersons.Any(a => a.ApplicationUserId == user.Id))) {
                return null;
            }

            var gameTeams = _uow.GetCustomRepository<IGameTeamRepository>().GetGameTeamsByGameId(id);

            GameTeam userTeam = gameTeams.Single(p => p.Team.TeamPersons.Exists(a => a.ApplicationUserId == user.Id));

            userTeam.ResultConfirmed = gameAccept.accepted;
            userTeam = _uow.GameTeams.Update(userTeam);
            if (ResultUtil.IsGameConfirmable(game))
            {
                AcceptGameAndChangeStandings(game);
            }

            _uow.SaveChanges();
            return _gameFactory.Create(game, _courtFactory.Create(game.Court));
        }

        public GameDTO GetGameById(long gameId)
        {
            var game = _uow.GetCustomRepository<IGameRepository>().GetGameById(gameId);

            return _gameFactory.Create(game, _courtFactory.Create(game.Court));
        }

        public GameTypeDTO AddGameType(GameTypeDTO type)
        {
            if (_uow.GetCustomRepository<IGameTypeRepository>().Exists(type.GameTypeName))
            {
                return type;
            }
            var gameType = _uow.GameTypes.Add(new GameType()
            {
                GameTypeName = type.GameTypeName
            });
            _uow.SaveChanges();
            return _gameFactory.Create(gameType);
        }

        public List<GameTypeDTO> GetAllGameTypes()
        {
            List<GameType> types = _uow.GameTypes.GetAll();
            List<GameTypeDTO> result = new List<GameTypeDTO>();
            foreach (GameType type in types)
            {
                result.Add(_gameFactory.Create(type));
            }
            return result;
        }

        public GameTypeDTO UpdateGameTypeById(long id, GameTypeDTO type)
        {
            GameType gameType = _uow.GameTypes.Find(id);
            gameType.GameTypeName = type.GameTypeName;
            var updated = _uow.GameTypes.Update(gameType);

            _uow.SaveChanges();
            return _gameFactory.Create(updated);
        }

        public GameDTO UpdateGameById(long id, GameDTO dto)
        {
            var game = _uow.GetCustomRepository<IGameRepository>().GetGameById(id);

            game.GameType = dto.GameType;
            game.ApplicationUserId = dto.RefereeId;
            game.GameTs = dto.GameTs;
            game.CourtId = dto.Court.CourtId;

            var result = _uow.Games.Update(game);
            _uow.SaveChanges();
            return _gameFactory.Create(result, _courtFactory.Create(game.Court));
        }

        private GameTeam BuildGameTeam(Game game, long teamId)
        {
            return new GameTeam()
            {
                GameId = game.GameId,
                TeamId = teamId
            };
        }

        private void AcceptGameAndChangeStandings(Game result)
        {
            result.ConfirmedTs = DateTime.Now;
            _uow.Games.Update(result);

            var team = result.GameTeams[0];
            var opponent = result.GameTeams[1];

            Boolean teamWon = team.Points > opponent.Points;
            UpdateGameTeam(team);
            UpdateGameTeam(opponent);

            if (result.GameType.GameTypeName == "Friendly")
            {
                return;
            }

            Standings homeTeamStandings = _uow.Standings.GetAll().Single(p => p.TeamId == team.TeamId);
            ModifyStandingsByResult(homeTeamStandings, teamWon);

            Standings awayTeamStandings = _uow.Standings.GetAll().Single(p => p.TeamId == opponent.TeamId);
            ModifyStandingsByResult(awayTeamStandings, !teamWon);
        }

        private void UpdateGameTeam(GameTeam team)
        {
            if (team.ResultConfirmed != true)
            {
                team.ResultConfirmed = true;
                _uow.GameTeams.Update(team);
            }
        }

        private void ModifyStandingsByResult(Standings standing, Boolean won)
        {
            standing.GamesPlayed += 1;
            standing.Losses = won ? standing.Losses : standing.Losses + 1;
            standing.Wins = won ? standing.Wins + 1 : standing.Wins;
            standing.Points = won ? standing.Points + 3 : standing.Points;

            _uow.Standings.Update(standing);
        }
    }
}
