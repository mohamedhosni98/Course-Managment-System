using CourseManagment.Models;

namespace CourseManagment.Repository.RepoManager
{
    public interface IStudentRepository
    {

        List<Student> Index();
        Student GetStudent(int id);
        void Create(Student student);

        void Update(int id, Student student);
        void Delete(int id);
    }
}
