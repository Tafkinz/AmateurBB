using System;
using System.Collections.Generic;
using System.Text;
using Model;

namespace BL.DTO
{
    public class StandingDTO
    {
        public string Team { get; set; }
        public int GamesPlayed { get; set; }
        public int Wins { get; set; }
        public int Losses { get; set; }

        public int Points { get; set; }
    }
}
