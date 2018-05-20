using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Model
{
    public class PersonType
    {
        public long PersonTypeId { get; set; }

        [MaxLength(256)]
        public string PersonTypeName { get; set; }

        public List<ApplicationUser> People = new List<ApplicationUser>();
    }
}
