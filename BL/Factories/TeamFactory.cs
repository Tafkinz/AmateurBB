using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BL.DTO;
using Model;

namespace BL.Factories
{
    public class TeamFactory : ITeamFactory
    {
        public TeamDTO Create(Team t, string manager, List<GameResultDTO> gameResults, StandingDTO standing)
        {
            return new TeamDTO()
            {
                City = t.City,
                TeamName = t.TeamName,
                Logo = t.Logo,
                TeamDisplayName = t.FullTeamName,
                TeamId = t.TeamId,
                GameResults = gameResults,
                Manager = manager,
                Standings = standing

            };
        }

        public Team Create(TeamDTO t)
        {
            return new Team()
            {
                City = t.City,
                Logo = t.Logo,
                TeamName = t.TeamName
            };
        }

        public TeamPersonDTO CreateFromTeamPersons(TeamPerson p)
        {
            return new TeamPersonDTO()
            {
                TeamPersonId = p.TeamPersonId,
                TeamPerson = p.Person.DisplayName,
                TeamPersonRole = p.Person.PersonType.PersonTypeName
            };
        }
    }
}
