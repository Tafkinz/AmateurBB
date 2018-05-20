using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Model
{
    public class TeamPerson
    {
        public long TeamPersonId { get; set; }

        public DateTime ValidFrom { get; set; }

        public DateTime ValidUntil { get; set; }

        [ForeignKey("Person")]
        public string ApplicationUserId { get; set; }
        public ApplicationUser Person { get; set; }

        public long TeamId { get; set; }
        public Team Team { get; set; }
    }
}
