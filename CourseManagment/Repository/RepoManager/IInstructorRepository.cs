using CourseManagment.Models;

namespace CourseManagment.Repository.RepoManager
{
    public interface IInstructorRepository
    {

        List<Instructor> Index();
        Instructor GetInstructor(int id);
        void Create(Instructor instructor);

        void Update(int id, Instructor instructor);
        void Delete(int id);
    }
}
