using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BL.DTO
{
    public class ContactsDTO
    {
        [Required]
        public long ContactTypeId { get; set; }

        public string ContactType { get; set; }
        [Required]
        [MaxLength(256)]
        public string Value { get; set; }

        public long ContactId { get; set; }
        [Required]
        public string UserId { get; set; }
    }
}
