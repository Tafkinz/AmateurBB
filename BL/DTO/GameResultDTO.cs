using System;
using System.Collections.Generic;
using System.Text;

namespace BL.DTO
{
    public class GameResultDTO
    {
        public string HomeTeam { get; set; }

        public string AwayTeam { get; set; }

        public string Referee { get; set; }

        public int HomeTeamPoints { get; set; }
        public int AwayTeamPoints { get; set; }

        public CourtDTO Court { get; set; }

        public long GameResultId { get; set; }
    }
}
