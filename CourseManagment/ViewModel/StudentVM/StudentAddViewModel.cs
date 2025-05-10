using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace CourseManagment.ViewModel.StudentVM
{
    public class StudentAddViewModel
    {
        [Required(ErrorMessage ="The Name Feild Is Required")]
        public string Name { get; set; }
        public string? Address { get; set; }
        public int DepartmentId { get; set; }
        public IEnumerable<SelectListItem>? DepartmentName { get; set; }
        public string? Image { get; set; }
        public IFormFile? ImageFile { get; set;  }
    }
}
