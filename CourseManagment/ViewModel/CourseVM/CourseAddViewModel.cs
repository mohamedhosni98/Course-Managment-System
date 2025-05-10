using CourseManagment.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace CourseManagment.ViewModel.CourseVM
{
    public class CourseAddViewModel
    {
      
        
            [Required(ErrorMessage = "Course name is required")]
            public string Name { get; set; }

            public string? Description { get; set; }

            [Required(ErrorMessage = "Degree is required")]
            [Range(0, 100, ErrorMessage = "Degree must be between 0 and 100")]
            public int Degree { get; set; }

            [Required(ErrorMessage = "Minimum degree is required")]
            [Range(0, 100, ErrorMessage = "Minimum degree must be between 0 and 100")]
            public int MinDegree { get; set; }

            [Required(ErrorMessage = "Please select a department")]
            public int DepartmentId { get; set; }

            public IEnumerable<SelectListItem>? Departments { get; set; }

            public IEnumerable<SelectListItem>? Instructors { get; set; }
            [Required(ErrorMessage = "Please select at least one instructor")]
            public List<int> SelectedInstructorIds { get; set; }

            public IEnumerable<SelectListItem>? Students { get; set; }
            public List<int> SelectedStudentIds { get; set; } // اختياري حسب الحاجة
        



    }
}
