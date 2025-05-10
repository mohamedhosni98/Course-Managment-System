using CourseManagment.Models;

namespace CourseManagment.Repository.RepoManager
{
    public interface ICourseRepository
    {
        // crud operation 
        List<Course> Index(); // all course  

        Course GetCourse(int id);  // dispaly course by id 
        void Create(Course course);  // insert new course
        void Update(int id , Course course); // edit course 

        void Delete(int id);  // delete course 
    }
}
