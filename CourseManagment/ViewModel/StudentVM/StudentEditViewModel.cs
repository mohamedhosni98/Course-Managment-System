using Microsoft.AspNetCore.Mvc.Rendering;

namespace CourseManagment.ViewModel.StudentVM
{
    public class StudentEditViewModel
    {
        
        public string Name { get; set; }
        public string? Address { get; set; }
        public int DepartmentId { get; set; }
        public IEnumerable<SelectListItem>? Departments { get; set; }
        public string? Image { get; set; }
        public IFormFile? ImageFile { get; set; }
    }
}
