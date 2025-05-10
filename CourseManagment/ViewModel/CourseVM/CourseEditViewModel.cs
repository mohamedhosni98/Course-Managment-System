using CourseManagment.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace CourseManagment.ViewModel.CourseVM
{
    public class CourseEditViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        [Range(0,100)]
        public int Degree { get; set; }
        [Range(0,50)]
        public int MinDegree { get; set; }
        [MaxLength(300)]
        public string? Description { get; set; }
        [ValidateNever]
        public  List<SelectListItem> DepartmentsItem { get; set; }
        public int DepartmentId { get; set; }

        public List<int> InstructorsId { get; set; } = new List<int>();
        [ValidateNever]
        public List<SelectListItem> InstructorsItem { get; set; }

        public List<int> StudentsId { get; set; } = new List<int>();
        [ValidateNever]
        public List<SelectListItem> StudentsItem { get; set; }

        public List<StudentWithGrade> StudentWithGrades { get; set; } = new(); 
    }
}
