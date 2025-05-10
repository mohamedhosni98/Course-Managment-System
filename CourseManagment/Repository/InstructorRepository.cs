using CourseManagment.Data;
using CourseManagment.Models;
using CourseManagment.Repository.RepoManager;
using Microsoft.EntityFrameworkCore;

namespace CourseManagment.Repository
{
    public class InstructorRepository : IInstructorRepository
    {
        private readonly ApplicationDbContext context;

        public InstructorRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public void Create(Instructor instructor)
        {
            context.Instructors.Add(instructor);
            context.SaveChanges();
        }

        public void Delete(int id)
        {
            var instructor = GetInstructor(id);
            context.Instructors.Remove(instructor);
            context.SaveChanges();

        }

        public Instructor GetInstructor(int id)
        {
            return context.Instructors.FirstOrDefault(i => i.Id==id); 
        }

        public List<Instructor> Index()
        {
            return context.Instructors
                .Include(i => i.department)
                .Include(i => i.CourseInstructors)
                .ToList();
        }

        public void Update(int id, Instructor instructor)
        {
            var oldinstructor = GetInstructor(id);
            oldinstructor.Name = instructor.Name; 
            oldinstructor.Address = instructor.Address; 
            oldinstructor.Salary = instructor.Salary; 
            oldinstructor.Image = instructor.Image; 
            oldinstructor.DepartmentId = instructor.DepartmentId;
            context.SaveChanges();

        }
    }
}
