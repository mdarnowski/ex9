using Lect_3.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace tut3.Models
{
    public class EnrollmentDbContext : DbContext
    {
        public DbSet<Enrollment> Enrollments { get; set; }

        public EnrollmentDbContext() { }
        public EnrollmentDbContext(DbContextOptions options) : base()
        {

        }
    }
}
