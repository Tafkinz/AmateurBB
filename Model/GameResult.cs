using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Drawing;
using System.Text;

namespace Model
{
    public class GameResult
    {
        public long GameResultId { get; set; }

        public Boolean RefereeConfirmed { get; set; }

        public Boolean HomeTeamConfirmed { get; set; }

        public Boolean AwayTeamConfirmed { get; set; }

        public DateTime ConfirmedTs { get; set; }

        public List<GameTeam> GameTeams { get; set; } = new List<GameTeam>();
    }
}
