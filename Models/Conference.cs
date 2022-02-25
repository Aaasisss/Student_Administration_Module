using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static System.Object;
namespace Seminar.Models
{
    public class Conference
    {
        [Key]
        public int conferenceID { get; set; }
        [Required]
        public string conferenceName { get; set; }
        [Required]
        public string conferenceVenue { get; set; }
        [Required]
        public string conferenceDate { get; set; }
        [Required]
        public double conferencePrice { get; set; }

        [ForeignKey("Organiser")]
        public int organiserID { get; set; }
        public virtual Organiser Organiser { get; set; }

        public virtual ICollection<Registration> Registrations { get; set; }

    }
}
