namespace CourseManagment.ViewModel.StudentVM
{
    public class StudentDetailsViewModel
    {
        public int Id { get; set; }
         
        public string Name { get; set; }
        public string? Address { get; set; }

        public string? Image { get; set; }
        public string DepartmentName { get; set; }
        public List<CourseWithGrade> CourseWithGrades { get; set; } = new List<CourseWithGrade>();

    }

    public class CourseWithGrade
    {
        public string CourseName { get; set; }
       
        public double Grade { get; set; }
    }

    
  
}
