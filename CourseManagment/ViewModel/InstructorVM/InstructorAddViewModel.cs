using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace CourseManagment.ViewModel.InstructorVM
{
    public class InstructorAddViewModel
    {
        public string Name { get; set; }

        public string? Address { get; set; }

        public decimal Salary { get; set; }

        public string? Image { get; set; }
        [Required(ErrorMessage = "Please upload an image.")]
        public IFormFile ImageFile { get; set; }
        public int DepartmentId { get; set; }
        public IEnumerable<SelectListItem>? Departments { get; set; }

    }
}
