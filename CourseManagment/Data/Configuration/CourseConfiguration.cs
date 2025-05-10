using CourseManagment.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CourseManagment.Data.Configuration
{
    public class CourseConfiguration : IEntityTypeConfiguration<Course>
    {
        public void Configure(EntityTypeBuilder<Course> builder)
        {
                builder.HasKey(c => c.Id);
                builder.Property(s => s.Name)
                    .HasColumnType("VARCHAR")
                    .HasMaxLength(50).IsRequired();
            builder.Property(c => c.Description)
                .HasColumnType("nvarchar(500)");
                

               
            }
        }
    }
