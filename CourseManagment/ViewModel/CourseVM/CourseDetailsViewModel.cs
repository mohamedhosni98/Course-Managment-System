using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace CourseManagment.ViewModel.CourseVM
{
    public class CourseDetailsViewModel
    {
        public string Name { get; set; }

        public int Degree { get; set; }

        public int MinDegree { get; set; }

        public string? Description { get; set; }
        
        public string? DepartmentName { get; set; }
        public List<string> InstructorName { get; set; } = new List<string>();
        public List<string> Students { get; set; } = new List<string>();
        public List<StudentWithGrade> studentWithGrades { get; set; } = new();  

    }

    public class StudentWithGrade
    { 
        public int StudentId { get; set; }
     public string? StudentName { get; set;  }
        public double Grade { get; set;  }
    }
}
