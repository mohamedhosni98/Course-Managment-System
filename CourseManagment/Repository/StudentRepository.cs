using CourseManagment.Data;
using CourseManagment.Models;
using CourseManagment.Repository.RepoManager;
using Microsoft.EntityFrameworkCore;

namespace CourseManagment.Repository
{
    public class StudentRepository : IStudentRepository
    {
        private readonly ApplicationDbContext context;

        public StudentRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public void Create(Student student)
        {
             context.Students.Add(student);
            context.SaveChanges(); 
        }

        public void Delete(int id)
        {
            var student = GetStudent(id);
            context.Students.Remove(student);
            context.SaveChanges(); 
        }

        public Student GetStudent(int id)
        {
            return context.Students.Include(s=>s.Department).Include(s=>s.StudentCourses).ThenInclude(sc=>sc.Course)
                .FirstOrDefault(s => s.Id == id);
        }

        public List<Student> Index()
        {
            return context.Students.
                Include(s=>s.Department)
                .Include(s=>s.StudentCourses)
                .ToList(); 
        }

        public void Update(int id, Student student)
        {
            var oldstudent = GetStudent(id);
            oldstudent.Name = student.Name;
            oldstudent.Address = student.Address;
            oldstudent.Image = student.Image;
            oldstudent.DepartmentId = student.DepartmentId;
            context.SaveChanges(); 

        }
    }
}
