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
            modelBuilder.Entity<Course>(p =>
            {
                p.ToTable("Courses");
                p.HasKey(p => p.Id);
                p.Property(p => p.Title).HasColumnType("VARCHAR(50)").IsRequired();
                p.Property(p => p.Duration).IsRequired();
                p.Property(p => p.Status).HasConversion<string>();
            })
        }
    }