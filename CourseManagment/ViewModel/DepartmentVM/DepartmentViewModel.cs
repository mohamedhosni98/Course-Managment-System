using System.ComponentModel.DataAnnotations;

namespace CourseManagment.ViewModel.DepartmentVM
{
    public class DepartmentViewModel
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Manager { get; set; }
    }
}
