using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BL.DTO
{
    public class ContactTypeDTO
    {
        [Required]
        [MaxLength(256)]
        public string ContactTypeName;
        public long ContactTypeId;
    }
}
