using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Model
{
    public class Game
    {
        public long GameId { get; set; }

        [ForeignKey("RefereeId")]
        public string ApplicationUserId { get; set; }
        public ApplicationUser Referee { get; set; }

        public long CourtId { get; set; }
        public Court Court { get; set; }

        public long GameTypeId { get; set; }
        public GameType GameType { get; set; }

        public DateTime GameTs { get; set; }

        public List<GameTeam> GameTeams { get; set; } = new List<GameTeam>();

        public Boolean RefereeConfirmed { get; set; }

        public DateTime ConfirmedTs { get; set; }
    }
}
