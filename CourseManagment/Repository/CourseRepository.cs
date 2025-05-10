using CourseManagment.Data;
using CourseManagment.Models;
using CourseManagment.Repository.RepoManager;
using Microsoft.EntityFrameworkCore;

namespace CourseManagment.Repository
{
    public class CourseRepository : ICourseRepository
    {
        private readonly ApplicationDbContext context;

        public CourseRepository(ApplicationDbContext _context)  // inject applicationdbcontext
        {
            this.context = _context;
        }

        public void Create(Course course)
        {
            context.Courses.Add(course);
            context.SaveChanges();
        }

        public void Delete(int id)
        {
            var course = GetCourse(id);
            context.Courses.Remove(course);
            context.SaveChanges(); 
        }

        public Course GetCourse(int id)
        {
            return context.Courses.Include(c=>c.department)
                .Include(c=>c.StudentCourses)
                .ThenInclude(sc=>sc.Student)
                .Include(c=>c.CourseInstructors)
                .ThenInclude(ci=>ci.Instructor)
                .FirstOrDefault(x => x.Id == id);
                

        }

        public List<Course> Index()
        {
            return context.Courses
                .Include(c => c.CourseInstructors)
                .Include(c => c.StudentCourses)
                .Include(c=>c.department)
                .ToList(); 
        }

        public void Update(int id, Course course)
        {
            var oldcourse = GetCourse(id);
            oldcourse.Name = course.Name;
            oldcourse.Degree = course.Degree;
            oldcourse.MinDegree = course.MinDegree;
            context.SaveChanges(); 
        }

        
    }
}
