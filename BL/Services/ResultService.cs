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
using Microsoft.AspNetCore.Identity;
using Model;

namespace BL.Services
{
    public class ResultService : IResultService
    {
        private readonly IAppUnitOfWork _uow;
        private readonly IStandingFactory _standingFactory;
        private readonly IGameFactory _gameFactory;
        private readonly ICourtFactory _courtFactory;
        private readonly IUserFactory _userFactory;
        private readonly IGameResultFactory _gameResultFactory;
        private readonly UserManager<ApplicationUser> _userManager;

        public ResultService(IAppUnitOfWork uow, IStandingFactory factory, IGameFactory gameFactory, ICourtFactory courtFactory, IUserFactory userFactory, UserManager<ApplicationUser> manager, IGameResultFactory gameResultFactory)
        {
            _uow = uow;
            _standingFactory = factory;
            _gameFactory = gameFactory;
            _courtFactory = courtFactory;
            _userFactory = userFactory;
            _userManager = manager;
            _gameResultFactory = gameResultFactory;
        }

        public async Task<List<StandingDTO>> GetAllStandingsAsync()
        {
            List<Standings> standingsList = await _uow.Standings.GetAllAsync();
            List<StandingDTO> result = new List<StandingDTO>();
            standingsList.ForEach(p => result.Add(_standingFactory.Create(p)));
            return result;
        }

        public List<StandingDTO> GetAllStandings()
        {
            List<Standings> standingsList = _uow.Standings.GetAll();
            List<StandingDTO> result = new List<StandingDTO>();
            standingsList.ForEach(p => result.Add(_standingFactory.Create(p)));
            return result;
        }

        public StandingDTO GetStandingsByTeamId(long teamId)
        {
            Standings standing = _uow.Standings.Find(teamId);
            return _standingFactory.Create(standing);
        }

        public GameDTO CreateGame(GameDTO game)
        {
            Game actualGame = _gameFactory.Create(game);
            actualGame = _uow.Games.Add(actualGame);

            GameResult gameResult = new GameResult();
            gameResult = _uow.GameResults.Add(gameResult);
            GameTeam gameTeamAway = BuildGameTeam(actualGame, gameResult.GameResultId, game.AwayTeamId, false);
            GameTeam gameTeamHome = BuildGameTeam(actualGame, gameResult.GameResultId, game.AwayTeamId, false);

            _uow.GameTeams.Add(gameTeamAway);
            _uow.GameTeams.Add(gameTeamHome);

            return _gameFactory.Create(actualGame, _courtFactory.Create(actualGame.Court),
                _userFactory.Create(actualGame.Referee), actualGame.GameType);
        }

        public async Task<List<GameResultActionDto>> GetUserPendingResults()
        {
            ApplicationUser user = await GetCurrentUser();
            if (!user.PersonType.PersonTypeName.Equals("manager")) throw new HttpRequestException("Only managers can view and accept results");

            Team team = _uow.TeamPersons.GetAll().Where(p => p.ApplicationUserId == user.Id).Single().Team;
            if (team == null) return null;
            List<GameTeam> homeGames = _uow.GameTeams.GetAll().Where(p => p.TeamId == team.TeamId).Where(p=> p.IsHomeTeam).Where(p => p.GameResult.HomeTeamConfirmed != true && p.GameResult.ConfirmedTs == null).ToList();
            List<GameTeam> awayGames = _uow.GameTeams.GetAll().Where(p => p.TeamId == team.TeamId).Where(p => !p.IsHomeTeam).Where(p => p.GameResult.AwayTeamConfirmed != true && p.GameResult.ConfirmedTs == null).ToList();

            List<GameResultActionDto> gameResults = new List<GameResultActionDto>();
            foreach (GameTeam game in homeGames)
            {
                GameTeam awayTeam = game.GameResult.GameTeams.Where(p => !p.IsHomeTeam).Single();
                var referee = game.Game.Referee;
                var court = game.Game.Court;
                gameResults.Add(_gameResultFactory.GetGameResultActionDto(game, awayTeam, referee, _courtFactory.Create(court)));
            }

            foreach (GameTeam game in awayGames)
            {
                GameTeam homeTeam = game.GameResult.GameTeams.Where(p => p.IsHomeTeam).Single();
                var referee = game.Game.Referee;
                var court = game.Game.Court;
                gameResults.Add(_gameResultFactory.GetGameResultActionDto(homeTeam, game, referee, _courtFactory.Create(court)));
            }
            return gameResults;
        }

        public async Task<GameResultActionDto> UpdateResult(long id, GameResultActionDto gameResult)
        {
            ApplicationUser user = await GetCurrentUser();

            GameResult result = _uow.GameResults.Find(id);
            if (result == null) throw new EntryPointNotFoundException();
            
            var gameTeams = _uow.GameTeams.GetAll().Where(p => p.GameResult == result);
            GameTeam userTeam = gameTeams.Single(p => p.Team.TeamPersons.Exists(a => a.ApplicationUserId == user.Id));
            if (userTeam.IsHomeTeam)
            {
                result.HomeTeamConfirmed = gameResult.Accept;
            }
            else
            {
                result.AwayTeamConfirmed = gameResult.Accept;
            }

            result = _uow.GameResults.Update(result);
            if (ResultUtil.IsGameConfirmable(result))
            {
                AcceptGameAndChangeStandings(result);
            }

            return gameResult;
        }

        private GameTeam BuildGameTeam(Game game, long gameResultId, long teamId, Boolean isHome)
        {
            return new GameTeam()
            {
                GameId = game.GameId,
                TeamId = teamId,
                GameResultId = gameResultId,
                IsHomeTeam = isHome
            };
        }

        private async Task<ApplicationUser> GetCurrentUser()
        {
            return await _userManager.GetUserAsync(ClaimsPrincipal.Current);
        }

        private void AcceptGameAndChangeStandings(GameResult result)
        {
            result.ConfirmedTs = DateTime.Now;
            _uow.GameResults.Update(result);

            var awayTeam = result.GameTeams.Single(p => !p.IsHomeTeam);
            var homeTeam = result.GameTeams.Single(p => p.IsHomeTeam);

            Boolean homeTeamWin = homeTeam.Points > awayTeam.Points;
            Standings homeTeamStandings = _uow.Standings.GetAll().Single(p => p.TeamId == homeTeam.TeamId);
            homeTeamStandings = ModifyStandingsByResult(homeTeamStandings, homeTeamWin);
            _uow.Standings.Update(homeTeamStandings);

            Standings awayTeamStandings = _uow.Standings.GetAll().Single(p => p.TeamId == awayTeam.TeamId);
            awayTeamStandings = ModifyStandingsByResult(awayTeamStandings, !homeTeamWin);
            _uow.Standings.Update(awayTeamStandings);
        }

        private Standings ModifyStandingsByResult(Standings standing, Boolean won)
        {
            standing.GamesPlayed += 1;
            standing.Losses = won ? standing.Losses : standing.Losses + 1;
            standing.Wins = won ? standing.Wins + 1 : standing.Wins;
            standing.Points = won ? standing.Points + 3 : standing.Points;

            return standing;
        }
    }
}
