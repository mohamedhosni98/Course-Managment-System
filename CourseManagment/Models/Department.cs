using System.ComponentModel.DataAnnotations;

namespace CourseManagment.Models
{
    public class Department
    {
        public int Id { get; set; }
        
        public string Name { get; set; }
        
        public string Manager { get; set; }

        // nav prop 
        public ICollection<Instructor> instructors { get; set; }
        public ICollection<Course> courses { get; set; }
        public ICollection<Student> students { get; set; }
    }
}
