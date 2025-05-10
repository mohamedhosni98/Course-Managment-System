using System.ComponentModel.DataAnnotations;

namespace CourseManagment.Models
{
    public class Course
    {
        public int Id { get; set; }

        public string Name { get; set; }
       
        public int Degree { get; set; }

        public int MinDegree { get; set; }

        public string? Description { get; set; }

        public List<CourseInstructor> CourseInstructors { get; set; } = new List<CourseInstructor>();
       public Department department { get; set; }
        public int DepartmentId { get; set; }

        public List<StudentCourse> StudentCourses { get; set; } = new List<StudentCourse>();
    }
}
