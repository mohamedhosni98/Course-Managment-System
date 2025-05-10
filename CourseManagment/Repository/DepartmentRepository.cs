using CourseManagment.Data;
using CourseManagment.Models;
using CourseManagment.Repository.RepoManager;
using Microsoft.EntityFrameworkCore;

namespace CourseManagment.Repository
{
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly ApplicationDbContext context;

        public DepartmentRepository(ApplicationDbContext context)
        {
            this.context = context;
        }
        public void Create(Department department)
        {
            context.Departments.Add(department);
            context.SaveChanges();
        }

        public void Delete(int id)
        {
           var department =GetDepartment(id);
            context.Departments.Remove(department);
            context.SaveChanges();
        }

        public Department GetDepartment(int id)
        {
            return context.Departments.FirstOrDefault(d => d.Id == id);
        }

        public List<Department> Index()
        {
            return context.Departments
                .Include(d => d.students)
                .Include(d => d.courses)
                .Include(d => d.instructors)
                .ToList();
        }

        public void Update(int id, Department newdepartment)
        {
            var olddepartment = GetDepartment(id);
            olddepartment.Name = newdepartment.Name;
            olddepartment.Manager = newdepartment.Manager;
            context.SaveChanges();
        }
    }
}
