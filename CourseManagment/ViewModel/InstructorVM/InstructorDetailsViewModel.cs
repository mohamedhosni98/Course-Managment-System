using System.ComponentModel.DataAnnotations.Schema;

namespace CourseManagment.ViewModel.InstructorVM
{
    public class InstructorDetailsViewModel
    {
        public int Id { get; set; }


        public string Name { get; set; }

        public string? Address { get; set; }

        public decimal Salary { get; set; }
        public string Image { get; set; }

        public int DepartmentId { get; set; }

    }
}
