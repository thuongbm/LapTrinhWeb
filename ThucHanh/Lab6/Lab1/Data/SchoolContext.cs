using Microsoft.EntityFrameworkCore;
using Lab1.Models;
using Microsoft.Identity.Client;

namespace Lab1.Data
{
    public class SchoolContext : DbContext
    {
        public SchoolContext(DbContextOptions<SchoolContext> options) : base(options) {}
        public virtual DbSet<Course> Courses { get; set; }
        public virtual DbSet<Learner> Learners { get; set; }
        public virtual DbSet<Enrollment> Enrollments { get; set; }
        public virtual DbSet<Major> Majors { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Major>().ToTable(nameof(Majors));
            modelBuilder.Entity<Learner>().ToTable(nameof(Learners));
            modelBuilder.Entity<Enrollment>().ToTable(nameof(Enrollments));
            modelBuilder.Entity<Course>().ToTable(nameof(Courses));
        }
    }
}
