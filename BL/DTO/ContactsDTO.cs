using System;
using System.Collections.Generic;
using System.Text;

namespace BL.DTO
{
    public class ContactsDTO
    {
        public long ContactTypeId { get; set; }

        public string ContactType { get; set; }

        public string Value { get; set; }

        public long ContactId { get; set; }

        public string UserId { get; set; }
    }
}
