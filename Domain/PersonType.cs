using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Domain
{
    public class PersonType
    {
        public long PersonTypeId { get; set; }

        [MaxLength(256)]
        public string PersonTypeName { get; set; }

        public List<Person> People = new List<Person>();
    }
}
