using Core.Data.Entity;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Database
{
    public class SchoolDbContext:DbContext
    {
        public SchoolDbContext(DbContextOptions<SchoolDbContext> options)
           : base(options) { }

        public virtual DbSet<Student> Students { get; set; }
        public virtual DbSet<Grade> Grades { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Grade>().ToTable("Grade");
            modelBuilder.Entity<Student>().ToTable("Student");
        }
    }
}
