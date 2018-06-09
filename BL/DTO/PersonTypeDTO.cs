using Model.Enum;
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
        public PersonTypes PersonTypeName;
        public long PersonTypeId;
    }
}
