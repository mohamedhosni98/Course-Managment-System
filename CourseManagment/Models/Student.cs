using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CourseManagment.Models
{
    public class Student
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string? Address { get; set; }

        public string? Image { get; set; }

        public List<StudentCourse> StudentCourses { get; set; } = new List<StudentCourse>();

        public int DepartmentId { get; set; }
        public Department Department { get; set; } 
    }
}
