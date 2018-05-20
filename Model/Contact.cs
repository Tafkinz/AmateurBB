using System;
using System.Collections.Generic;
using System.Text;
using Model;

namespace Model
{
    public class Contact
    {
        public long ContactId { get; set; }

        public string ContactValue { get; set; }

        public string ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }

        public long ContactTypeId { get; set; }
        public ContactType ContactType { get; set; }
    }
}
