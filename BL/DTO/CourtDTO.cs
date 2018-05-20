using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore.ValueGeneration.Internal;

namespace BL.DTO
{
    public class CourtDTO
    {
        public long CourtId { get; set; }
        public string Name { get; set; }

        public int Capacity { get; set; }
        public string Location { get; set; }
    }
}
