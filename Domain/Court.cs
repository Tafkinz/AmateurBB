using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Domain
{
    public class Court
    {
        public long CourtId { get; set; }

        [MaxLength(256)]
        [MinLength(3)]
        public string CourtName { get; set; }


        [Range(minimum: 0, maximum: 1000000, ErrorMessage = "Court capacity must be between 0 and million" )]
        public int Capacity { get; set; }

        [MaxLength(256)]
        public string Location { get; set; }

        public List<Game> Games = new List<Game>();
    }
}
