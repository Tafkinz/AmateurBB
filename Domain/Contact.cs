using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class Contact
    {
        public long ContactId { get; set; }

        public string ContactValue { get; set; }

        public long PersonId { get; set; }
        public Person Person { get; set; }

        public long ContactTypeId { get; set; }
        public ContactType ContactType { get; set; }
    }
}
