using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BL.DTO
{
    public class GameTypeDTO
    {
        [Required]
        [MaxLength(256)]
        public string GameTypeName;
        public long GameTypeId;
    }
}
