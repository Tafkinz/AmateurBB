using System;
using System.Collections.Generic;
using System.Text;

namespace BL.DTO
{
    public class TeamDTO
    {
        public long TeamId { get; set; }

        public string TeamName { get; set; }

        public string City { get; set; }

        public string Logo { get; set; }

        public string TeamDisplayName { get; set; }

        public string Manager { get; set; }

        public List<GameDTO> GameResults { get; set; }

        public StandingDTO Standings { get; set; }
    }
}
