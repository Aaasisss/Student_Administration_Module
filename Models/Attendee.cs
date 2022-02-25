using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using static System.Object;

namespace Seminar.Models
{
    public class Attendee
    {
        [Key]
        public int attendeeID { get; set; }
        [Required]
        public string attendeeFirstName { get; set; }
        [Required]
        public string attendeeLastName { get; set; }
        [Required]
        public int attendeeAge { get; set; }
        [Phone]
        public string attendeePhone { get; set; }
        [EmailAddress]
        public string attendeeEmail { get; set; }
        [Required]
        public string attendeeAddress { get; set; }

        public virtual ICollection<Registration> Registrations { get; set; }
    }
}
