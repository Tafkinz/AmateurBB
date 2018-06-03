using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BL.DTO
{
    public class PersonTypeDTO
    {
        [Required]
        [MaxLength(256)]
        public string PersonTypeName;
        public long PersonTypeId;
    }
}
