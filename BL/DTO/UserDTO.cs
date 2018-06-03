using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Model;

namespace BL.DTO
{
    public class UserDTO
    {
        [Required]
        [MaxLength(256)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(256)]
        public string LastName { get; set; }

        public PersonType PersonType { get; set; }

        public List<ContactsDTO> Contacts { get; set; }

        public string UserId { get; set; }
    }
}
