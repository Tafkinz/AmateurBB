using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Domain
{
    public class ContactType
    {
        public long ContactTypeId { get; set; }

        [MaxLength(256)]
        public string ContactTypeName { get; set; }

        public List<Contact> Contacts = new List<Contact>();
    }
}
