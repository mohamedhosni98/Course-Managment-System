using AutoMapper;
using CourseManagment.Models;
using CourseManagment.ViewModel.StudentVM;

namespace CourseManagment.Maping
{
    public class StudentProfile : Profile
    {
        public StudentProfile()
        {
            CreateMap<Student, StudentIndexViewModel>()
                .ForMember(des => des.CourseCount, opt => opt.MapFrom(src => src.StudentCourses.Count))
                .ForMember(des => des.DepartmentName, opt => opt.MapFrom(src => src.Department.Name));
            // maping from student to studentviewmodel 
            CreateMap<Student, StudentAddViewModel>()
                .ForMember(des=>des.ImageFile,opt=>opt.Ignore());
            // maping from studntviewmodel to student 
            CreateMap<StudentAddViewModel, Student>()
                .ForMember(des => des.StudentCourses, opt => opt.Ignore())
                .ForMember(des => des.Department, opt => opt.Ignore())
                .ForMember(des=>des.Image,opt=>opt.Ignore());

            // maping between student and studentdetails viewmodel
            CreateMap<Student, StudentDetailsViewModel>()
                .ForMember(des => des.DepartmentName, opt => opt.MapFrom(src => src.Department.Name))
                .ForMember(des => des.CourseWithGrades, opt => opt.MapFrom(src => src.StudentCourses
                .Select(sc => new CourseWithGrade { CourseName = sc.Course.Name, Grade = sc.grade }).ToList()));


            // maping between student and studenteditvoewmodel and reverse 
            CreateMap<Student, StudentEditViewModel>();
               // .ForMember(des=>des.DepartmentName,opt=>opt.MapFrom(src=>src.Department.Name));

            CreateMap<StudentEditViewModel, Student>()
                .ForMember(des => des.StudentCourses, opt => opt.Ignore())
                .ForMember(des => des.Department, opt => opt.Ignore())
                .ForMember(des=>des.Image,opt=>opt.Ignore());
        }
    }
}