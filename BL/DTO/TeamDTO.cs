using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BL.DTO
{
    public class TeamDTO
    {
        public long TeamId { get; set; }

        [Required]
        [MaxLength(256)]
        public string TeamName { get; set; }
        [MaxLength(256)]
        public string City { get; set; }
        [MaxLength(1024)]
        public string Logo { get; set; }

        public string TeamDisplayName { get; set; }

        public string Manager { get; set; }

        public List<GameDTO> GameResults { get; set; }

        public StandingDTO Standings { get; set; }
    }
}
