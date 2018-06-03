using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Microsoft.EntityFrameworkCore.ValueGeneration.Internal;

namespace BL.DTO
{
    public class CourtDTO
    {
        public long CourtId { get; set; }
        [Required]
        [MaxLength(256)]
        public string Name { get; set; }

        [Range(minimum: 0, maximum: 1000000, ErrorMessage = "Court capacity must be between 0 and million")]
        public int Capacity { get; set; }
        [MaxLength(256)]
        public string Location { get; set; }
    }
}
