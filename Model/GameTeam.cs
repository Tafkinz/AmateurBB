using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Model
{
    public class GameTeam
    {
        public long GameTeamId { get; set; }

        public long TeamId { get; set; }
        public Team Team { get; set; }

        public long GameId { get; set; }
        public Game Game { get; set; }

        [Range(minimum:0, maximum:1000)]
        public int Points { get; set; }

        public Boolean ResultConfirmed { get; set; }

    }
}
