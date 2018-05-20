using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Domain
{
    public class Team
    {
        public long TeamId { get; set; }

        [MaxLength(256)]
        [MinLength(3)]
        public string TeamName { get; set; }

        [MaxLength(256)]
        [MinLength(2)]
        public string City { get; set; }

        [MaxLength(256)]
        [MinLength(3)]
        public string Logo { get; set; }

        public string FullTeamName => $"{City} {TeamName}";

        public List<TeamPerson> TeamPersons { get; set; } = new List<TeamPerson>();

        public List<GameTeam> GameTeams { get; set; } = new List<GameTeam>();
    }
}
