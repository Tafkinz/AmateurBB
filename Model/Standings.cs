using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Model
{
    public class Standings
    {
        public long StandingsId { get; set; }

        public int Wins { get; set; }


        public int Losses { get; set; }

        public int GamesPlayed { get; set; }

        public int Points { get; set; }

        public long TeamId { get; set; }

        public Team Team { get; set; }
    }
}
