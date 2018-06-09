using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BL.DTO;
using BL.Factories;
using BL.Util;
using DAL.App.Interfaces;
using DAL.App.Interfaces.Repositories;
using Microsoft.Extensions.Logging;
using Model;

namespace BL.Services
{
    [SuppressMessage("ReSharper", "ReplaceWithSingleCallToSingle")]
    public class TeamService : ITeamService
    {
        private readonly IAppUnitOfWork _uow;
        private readonly ITeamFactory _teamFactory;
        private readonly ICourtFactory _courtFactory;
        private readonly IStandingFactory _standingFactory;
        private readonly AuthUtil _auth;

        public TeamService(IAppUnitOfWork uow, ITeamFactory factory, ICourtFactory courtFactory, IStandingFactory standingFactory, AuthUtil auth)
        {
            _uow = uow;
            _teamFactory = factory;
            _courtFactory = courtFactory;
            _standingFactory = standingFactory;
            _auth = auth;
        }
        public TeamDTO AddTeam(TeamDTO teamDto)
        {
            //Create team from DTO and add
            Team team = _teamFactory.Create(teamDto);

            //Create standings with 0 values for team
            team = _uow.Teams.Add(team);
            CreateStandingsForTeam(team);

            _uow.SaveChanges();
            //Save and return
            return GetTeamById(team.TeamId);
        }

        public TeamDTO GetTeamById(long teamId)
        {
            //Find team
            var team = _uow.Teams.Find(teamId);
            if (team == null) return null;
            string manager;
            //Try to get manager or just put no manager found when none
            try
            {
                manager = team.TeamPersons.Where(p =>
                    p.Person.PersonType.PersonTypeName.Equals("manager")).Single().Person.DisplayName;
            }
            catch (Exception e)
            {
                manager = "No manager found";
                Logger<TeamService> logger = new Logger<TeamService>(new LoggerFactory());
                logger.LogTrace(e.StackTrace);
            }

            //Find games for team and create DTOs
            List<long> games = new List<long>();
            team.GameTeams.ForEach(p => games.Add(p.GameId));
            List<GameDTO> gameResults = new List<GameDTO>();


            foreach (long gameId in games)
            {
                gameResults.Add(GetGameResult(gameId));
            }

            //Get standings
            StandingDTO standing = null;
            Standings standings = _uow.Standings.GetAll().Where(p => p.TeamId == teamId).Single();
            if (standings != null)
            {
                standing = _standingFactory.Create(standings);
            };

            //Make DTO and return
            TeamDTO teamDto = _teamFactory.Create(team, manager, gameResults, standing);
            return teamDto;
        }

        public List<TeamDTO> GetAllTeams()
        {
            //Find list of teams and get each team DTO from upper method
            List<long> teamIds = new List<long>();
            _uow.Teams.GetAll().ForEach(p => teamIds.Add(p.TeamId));

            List<TeamDTO> teams = new List<TeamDTO>();
            teamIds.ForEach(p => teams.Add(GetTeamById(p)));
            return teams;
        }

        public TeamDTO UpdateTeam(long id, TeamDTO team)
        {
            //Update team with specified values and return
            var teamToUpdate = _uow.Teams.Find(id);
            teamToUpdate.City = team.City;
            teamToUpdate.Logo = team.Logo;
            teamToUpdate.TeamName = team.TeamName;

            _uow.Teams.Update(teamToUpdate);
            return GetTeamById(teamToUpdate.TeamId);
        }

        public async Task<List<TeamPersonDTO>> GetAllTeamPersonsAsync(long teamId)
        {
            //Find team related persons and create DTOs from each, then return
            var team = await _uow.Teams.GetAllAsync();
            List<TeamPerson> teamPersons = team.Where(p => p.TeamId == teamId).Single().TeamPersons.Where(p => p.ValidUntil > DateTime.Now).ToList();
            List<TeamPersonDTO> teamPersonsResult = new List<TeamPersonDTO>();
            teamPersons.ForEach(p => teamPersonsResult.Add(_teamFactory.CreateFromTeamPersons(p)));

            return teamPersonsResult;
        }

        public TeamPersonDTO GetTeamPersonById(long id)
        {
            //Find team person and return, null if not found
            TeamPerson teamPerson = _uow.TeamPersons.Find(id);
            if (teamPerson == null) return null;
            return _teamFactory.CreateFromTeamPersons(teamPerson);
        }

        public void PutPersonToTeam(long teamId, string userId, bool isManager)
        {
            
            var user = _uow.Users.Find(userId);
            var currentUserId = _auth.GetCurrentUserId();
            var currentUser = _uow.GetCustomRepository<IUserRepository>().FindById(currentUserId);

            var team = _uow.GetCustomRepository<ITeamRepository>().FindById(teamId);
            if (!team.TeamPersons.Any(p => p.ApplicationUserId == currentUserId))
            {
                throw new Exception("User is not manager of team " + team.FullTeamName);
            }
            if (isManager)
            {
                long personTypeId = _uow.PersonTypes.GetAll().Where(p => p.PersonTypeName == Model.Enum.PersonTypes.Manager).Single().PersonTypeId;
                user.PersonTypeId = personTypeId;
            }
            else
            {
                long personTypeId = _uow.PersonTypes.GetAll().Where(p => p.PersonTypeName == Model.Enum.PersonTypes.Player).Single().PersonTypeId;
                user.PersonTypeId = personTypeId;
            }

            _uow.Users.Update(user);
            var teamPerson = _uow.TeamPersons.GetAll().Where(p => p.ApplicationUserId == userId && p.TeamId == teamId && p.ValidUntil > DateTime.Now).Single();

            if (teamPerson == null)
            {
                teamPerson = new TeamPerson()
                {
                    ApplicationUserId = userId,
                    TeamId = teamId,
                    ValidFrom = DateTime.Now
                };
                _uow.TeamPersons.Add(teamPerson);
                return;
            }

            _uow.TeamPersons.Update(teamPerson);
            _uow.SaveChanges();
        }

        public void RemovePersonFromTeam(long teamId, string userId)
        {
            var teamPerson = _uow.TeamPersons.GetAll().Where(p => p.ApplicationUserId == userId && p.TeamId == teamId).Single();
            if (teamPerson == null) throw new KeyNotFoundException("Connection between user " + userId + " and team " + teamId + " not found");
            teamPerson.ValidUntil = DateTime.Now;

            _uow.TeamPersons.Update(teamPerson);
            _uow.SaveChanges();
        }

        private void CreateStandingsForTeam(Team team)
        {
            Standings standings = new Standings()
            {
                TeamId = team.TeamId,
                GamesPlayed = 0,
                Wins = 0,
                Losses = 0,
                Points = 0
            };
            _uow.Standings.Add(standings);
        }

        private GameDTO GetGameResult(long gameId)
        {
            Game game = _uow.Games.GetAll().Single(p => p.GameId == gameId);
            List<GameTeam> gameTeams = game.GameTeams;
            var referee = game.Referee.DisplayName;

            GameTeam homeTeam = gameTeams[0];
            int homeTeamPoints = homeTeam.Points;
            string homeTeamName = homeTeam.Team.FullTeamName;

            GameTeam awayTeam = gameTeams[1];
            int awayTeamPoints = awayTeam.Points;
            string awayTeamName = awayTeam.Team.FullTeamName;

            return new GameDTO()
            {
                AwayTeamId = awayTeam.TeamId,
                AwayTeamName = awayTeamName,
                AwayTeamPoints = awayTeamPoints,
                Court = _courtFactory.Create(game.Court),
                HomeTeamId = homeTeam.TeamId,
                HomeTeamName = homeTeamName,
                HomeTeamPoints = homeTeamPoints,
                RefereeId = game.Referee.Id,
                Referee = referee
            };
        }
    }
}
