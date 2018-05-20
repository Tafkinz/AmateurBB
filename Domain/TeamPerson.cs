using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain
{
    public class TeamPerson
    {
        public long TeamPersonId { get; set; }

        public DateTime ValidFrom { get; set; }

        public DateTime ValidUntil { get; set; }

        public long PersonId { get; set; }
        public Person Person { get; set; }

        public long TeamId { get; set; }
        public Team Team { get; set; }
    }
}
