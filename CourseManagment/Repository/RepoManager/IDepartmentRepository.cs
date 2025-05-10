using CourseManagment.Models;

namespace CourseManagment.Repository.RepoManager
{
    public interface IDepartmentRepository
    {
        List<Department> Index(); 
        Department GetDepartment(int id);
        void Create(Department department);

        void Update(int id, Department newdepartment);
        void Delete(int id); 

        

    }
}
