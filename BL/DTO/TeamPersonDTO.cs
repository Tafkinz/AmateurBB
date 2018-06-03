using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BL.DTO
{
    public class TeamPersonDTO
    {
        public long TeamPersonId;

        public string TeamPerson { get; set; }
        [MaxLength(256)]
        public string TeamPersonRole { get; set; }
        public long PersonTypeId { get; set; }
    }
}
