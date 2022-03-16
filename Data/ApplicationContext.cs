using Microsoft.EntityFrameworkCore;

namespace CourseManager.Data
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Course> Courses { get; set; }
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data source=(localdb)\\mssqllocaldb;Initial Catalog=CourseManager;Integrated Security=true");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CourseConfiguration().Assembly);
        }
    }