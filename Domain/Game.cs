using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain
{
    public class Game
    {
        public long GameId { get; set; }

        [ForeignKey("RefereeId")]
        public long PersonId { get; set; }
        public Person Referee { get; set; }

        public long CourtId { get; set; }
        public Court Court { get; set; }

        public long GameTypeId { get; set; }
        public GameType GameType { get; set; }

        public DateTime GameTs { get; set; }
    }
}
