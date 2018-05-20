using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Drawing;
using System.Text;

namespace Domain
{
    public class GameResult
    {
        public long GameResultId { get; set; }

        public bool RefereeConfirmed { get; set; }

        public bool HomeTeamConfirmed { get; set; }

        public bool AwayTeamConfirmed { get; set; }

        public DateTime ConfirmedTs { get; set; }

        public List<GameTeam> GameTeams { get; set; } = new List<GameTeam>();
    }
}
