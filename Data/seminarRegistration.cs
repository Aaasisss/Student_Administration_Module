using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Seminar.Models;

namespace Seminar.Data
{
    public class seminarRegistration : DbContext
    {
        public seminarRegistration (DbContextOptions<seminarRegistration> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<Seminar.Models.Attendee> Attendee { get; set; }

        public DbSet<Seminar.Models.Conference> Conference { get; set; }

        public DbSet<Seminar.Models.Organiser> Organiser { get; set; }

        public DbSet<Seminar.Models.Registration> Registration { get; set; }
    }
}
