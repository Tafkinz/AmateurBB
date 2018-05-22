using System;
using System.Collections.Generic;
using System.Text;
using Model;

namespace BL.DTO
{
    public class GameDTO
    {
        public long GameId { get; set; }
        public long AwayTeamId { get; set; }
        public long HomeTeamId { get; set; }

        public string HomeTeamName { get; set; }

        public string AwayTeamName { get; set; }

        public int AwayTeamPoints { get; set; }

        public int HomeTeamPoints { get; set; }
        public CourtDTO Court { get; set; }

        public GameType GameType { get; set; }

        public string Referee { get; set; }

        public string RefereeId { get; set; }

        public DateTime GameTs { get; set; }
    }
}
