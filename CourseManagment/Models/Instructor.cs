using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection.Metadata;

namespace CourseManagment.Models
{
    public class Instructor
    {
        public int Id { get; set; }

        
        public string Name { get; set; }

        public string? Address { get; set; }

        public decimal Salary { get; set; }
        [Column(TypeName ="nvarchar(200)")]
        public string Image { get; set; }

        // Navigation Property 
        public Department department { get; set; } 
        public int DepartmentId { get; set;  }

        // navigation prop
        public List<CourseInstructor> CourseInstructors { get; set; } = new List<CourseInstructor>();
    }
}
