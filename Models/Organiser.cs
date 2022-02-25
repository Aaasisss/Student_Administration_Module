using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using static System.Object;

namespace Seminar.Models
{
    public class Organiser
    {
        [Key]
        public int organiserID { get; set; }
        [Required]
        public string organiserName { get; set; }
        [Required]
        public string organiserLocation { get; set; }
        [Phone]
        public string organiserPhone { get; set; }
        [EmailAddress]
        public string organiserEmail { get; set; }

        public virtual ICollection<Conference> Conferences { get; set; }
    }
}
