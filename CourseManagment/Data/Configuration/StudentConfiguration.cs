using CourseManagment.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CourseManagment.Data.Configuration
{
    public class StudentConfiguration : IEntityTypeConfiguration<Student>
    {
        public void Configure(EntityTypeBuilder<Student> builder)
        {
            builder.HasKey(s => s.Id);
            builder.Property(s => s.Name)
                .HasColumnType("VARCHAR")
                .HasMaxLength(50).IsRequired();

            builder.Property(s => s.Address)
                .HasColumnType("VARCHAR")
                .HasMaxLength(60);

            builder.Property(s => s.Image).HasColumnType("nvarchar(200)");

            builder.HasOne(s => s.Department)
                .WithMany(d => d.students)
                .HasForeignKey(s => s.DepartmentId)
                .IsRequired();
        }
    }
}
