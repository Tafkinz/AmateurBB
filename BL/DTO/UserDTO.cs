using System;
using System.Collections.Generic;
using System.Text;
using Model;

namespace BL.DTO
{
    public class UserDTO
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public PersonType PersonType { get; set; }

        public List<ContactsDTO> Contacts { get; set; }

        public string UserId { get; set; }
    }
}
