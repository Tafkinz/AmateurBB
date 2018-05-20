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

        public CourtDTO Court { get; set; }

        public GameType GameType { get; set; }

        public UserDTO Referee { get; set; }

        public DateTime GameTs { get; set; }
    }
}
