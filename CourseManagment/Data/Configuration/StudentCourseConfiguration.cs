using CourseManagment.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CourseManagment.Data.Configuration
{
    public class StudentCourseConfiguration : IEntityTypeConfiguration<StudentCourse>
    {
        public void Configure(EntityTypeBuilder<StudentCourse> builder)
        {
            builder.HasKey(x => new { x.StudentId, x.CourseId });
            builder.HasOne(x => x.Student).WithMany(s => s.StudentCourses).HasForeignKey(x => x.StudentId);
            builder.HasOne(x => x.Course).WithMany(s => s.StudentCourses).HasForeignKey(x => x.CourseId);
        }
    }
}
