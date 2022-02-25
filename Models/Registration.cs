using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static System.Object;



namespace Seminar.Models
{
    public class Registration
    {
        [Key]
        public int registrationID { get; set; }

        [ForeignKey("Attendee")]
        public int attendeeID { get; set; }
        public virtual Attendee Attendee { get; set; }
        //public string attendeeName { get; set; }

        [ForeignKey("Conference")]
        public int conferenceID { get; set; }
        public virtual Conference Conference { get; set; }
        //public string conferenceName { get; set; }

    }
}
