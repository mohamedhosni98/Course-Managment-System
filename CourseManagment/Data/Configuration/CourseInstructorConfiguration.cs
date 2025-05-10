using CourseManagment.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CourseManagment.Data.Configuration
{
    public class CourseInstructorConfiguration : IEntityTypeConfiguration<CourseInstructor>
    {

        public void Configure(EntityTypeBuilder<CourseInstructor> builder)
        {
            builder.HasKey(x => new { x.InstructorId,x.CourseId });

            builder.HasOne(x => x.Instructor)
                .WithMany(i => i.CourseInstructors)
                .HasForeignKey(x => x.InstructorId);

            builder.HasOne(x => x.Course)
                .WithMany(c => c.CourseInstructors)
                .HasForeignKey(x => x.CourseId); 
        }
    }
}
