using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Domain
{
    public class GameType
    {
        public long GameTypeId { get; set; }

        [MaxLength(256)]
        public string GameTypeName { get; set; }

        public List<Game> Games { get; set; } = new List<Game>();
    }
}
