using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace CourseManagment.ViewModel.InstructorVM
{
    public class InstructorEditViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "الاسم مطلوب")]
        public string Name { get; set; }

        [Required(ErrorMessage = "العنوان مطلوب")]
        public string Address { get; set; }

        [Required(ErrorMessage = "الراتب مطلوب")]
        [Range(0, double.MaxValue, ErrorMessage = "الراتب يجب أن يكون قيمة موجبة")]
        public decimal Salary { get; set; }

        public string? Image { get; set; } // مسار الصورة الحالية

        [Display(Name = "الصورة")]
        public IFormFile? ImageFile { get; set; } // ملف الصورة (اختياري)

        [Required(ErrorMessage = "القسم مطلوب")]
        public int DepartmentId { get; set; }

        public IEnumerable<SelectListItem>? Departments { get; set; }
    }
}

