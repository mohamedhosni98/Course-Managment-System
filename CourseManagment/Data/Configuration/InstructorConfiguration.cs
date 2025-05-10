using CourseManagment.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CourseManagment.Data.Configuration
{
    public class InstructorConfiguration : IEntityTypeConfiguration<Instructor>
    {
        public void Configure(EntityTypeBuilder<Instructor> builder)
        {
            builder.HasKey(i => i.Id);
            builder.Property(s => s.Name)
                .HasColumnType("VARCHAR")
                .HasMaxLength(50).IsRequired();

            builder.Property(i => i.Address)
                .HasColumnType("VARCHAR")
                .HasMaxLength(60);

           // builder.Property(i => i.Image)
             //   .HasColumnType("VARBINARY(MAX)");

            builder.HasOne(i => i.department)
                .WithMany(d => d.instructors)
                .HasForeignKey(i => i.DepartmentId)
                .IsRequired();
        }
    }
}
