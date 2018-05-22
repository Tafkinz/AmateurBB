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

        public async Task<GameDTO> CreateGame(GameDTO gameDto)
        {
            Game game = _gameFactory.Create(gameDto);
            game = _uow.Games.Add(game);

            GameTeam gameTeam1 = BuildGameTeam(game, gameDto.AwayTeamId);
            GameTeam gameTeam2 = BuildGameTeam(game, gameDto.HomeTeamId);

            await _uow.GameTeams.AddAsync(gameTeam1);
            await _uow.GameTeams.AddAsync(gameTeam2);

            game = _uow.Games.Find(game.GameId);
            return _gameFactory.Create(game, _courtFactory.Create(game.Court),
                game.Referee, game.GameType);
        }

        public async Task<List<GameActionDto>> GetUserPendingResults()
        {
            ApplicationUser user = await GetCurrentUser();
            if (!user.PersonType.PersonTypeName.Equals("manager")) throw new HttpRequestException("Only managers can view and accept results");

            Team team = _uow.TeamPersons.GetAll().Single(p => p.ApplicationUserId == user.Id).Team;
            if (team == null) return null;
            List<GameTeam> unapprovedGames = _uow.GameTeams.GetAll().Where(p => p.TeamId == team.TeamId).Where(p => p.ResultConfirmed != true).ToList();

            List<GameActionDto> gameResults = new List<GameActionDto>();
            foreach (GameTeam game in unapprovedGames)
            {
                GameTeam oppisingTeam = game.Game.GameTeams.Single(p => !p.Equals(game));
                var referee = game.Game.Referee;
                var court = game.Game.Court;
                gameResults.Add(_gameResultFactory.GetGameResultActionDto(game, oppisingTeam, referee, _courtFactory.Create(court)));
            }

            return gameResults;
        }

        public async Task<GameActionDto> UpdateResult(long id, GameActionDto gameDto)
        {
            ApplicationUser user = await GetCurrentUser();

            Game game = _uow.Games.Find(id);
            if (game == null) throw new EntryPointNotFoundException();
            
            var gameTeams = _uow.GameTeams.GetAll().Where(p => p.GameId == game.GameId);
            GameTeam userTeam = gameTeams.Single(p => p.Team.TeamPersons.Exists(a => a.ApplicationUserId == user.Id));

            userTeam.ResultConfirmed = gameDto.Accept;
            userTeam = _uow.GameTeams.Update(userTeam);
            if (ResultUtil.IsGameConfirmable(game))
            {
                AcceptGameAndChangeStandings(game);
            }

            return gameDto;
        }

        private GameTeam BuildGameTeam(Game game, long teamId)
        {
            return new GameTeam()
            {
                GameId = game.GameId,
                TeamId = teamId
            };
        }

        private async Task<ApplicationUser> GetCurrentUser()
        {
            return await _userManager.GetUserAsync(ClaimsPrincipal.Current);
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
