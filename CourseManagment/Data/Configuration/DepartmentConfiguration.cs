using CourseManagment.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CourseManagment.Data.Configuration
{
    public class DepartmentConfiguration : IEntityTypeConfiguration<Department>
    {
        public void Configure(EntityTypeBuilder<Department> builder)
        {
            builder.HasKey(d => d.Id);
            builder.Property(d => d.Name)
                .HasColumnType("VARCHAR")
                .HasMaxLength(50)
                .IsRequired(); 

            builder.Property(d => d.Name)
                .HasColumnType("VARCHAR")
                .HasMaxLength(50)
                .IsRequired(); 



        }
    }
}
