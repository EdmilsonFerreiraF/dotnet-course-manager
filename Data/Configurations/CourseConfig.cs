using CourseManager.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CourseManager.Data.Configurations
{
    public class CourseConfiguration : IEntityTypeConfiguration<Course>
    {
        public void Configure(EntityTypeBuilder<Course> builder)
        {
            builder.ToTable("Courses");
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Title).HasColumnType("VARCHAR(50)").IsRequired();
            builder.Property(p => p.Duration).IsRequired();
            builder.Property(p => p.Status).HasConversion<string>();
        }
    }
}