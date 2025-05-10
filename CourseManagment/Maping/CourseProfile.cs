using AutoMapper;
using CourseManagment.Models;
using CourseManagment.ViewModel.CourseVM;
using CourseManagment.ViewModel.StudentVM;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CourseManagment.Maping
{
    public class CourseProfile : Profile
    {
        public CourseProfile()
        {
            // map between course and CourseIndexViewModel
            CreateMap<Course, CourseIndexViewModel>()
                .ForMember(des => des.DepartmentName, opt => opt.MapFrom(src => src.department.Name));
            //*********************************

            //map between course and courseaddviewmodel and revrese 
            CreateMap<Course, CourseAddViewModel>()
                 .ForMember(des => des.SelectedInstructorIds, opt => opt.MapFrom(src => src.CourseInstructors.Select(c => c.InstructorId).ToList()))
                 .ForMember(des => des.SelectedStudentIds, opt => opt.MapFrom(src => src.StudentCourses.Select(s => s.StudentId).ToList()))
                 .ForMember(des => des.Departments, opt => opt.Ignore())
                 .ForMember(des => des.Instructors, opt => opt.Ignore())
                 .ForMember(des => des.Description, opt => opt.MapFrom(src => src.Description))
                 .ForMember(des => des.Students, opt => opt.Ignore());

            CreateMap<CourseAddViewModel, Course>()
               .ForMember(des => des.CourseInstructors, opt => opt
               .MapFrom(src => src.SelectedInstructorIds
               .Select(id => new CourseInstructor { InstructorId = id })))
               .ForMember(des => des.StudentCourses, opt => opt
               .MapFrom(src => src.SelectedStudentIds
               .Select(id => new StudentCourse { StudentId = id })))
               .ForMember(des => des.Id, opt => opt.Ignore()).
               ForMember(des => des.department, opt => opt.Ignore());

            //**************************************************************************************
            //map between course and coursedetailsviewmodel
            CreateMap<Course, CourseDetailsViewModel>()
                .ForMember(des => des.DepartmentName, opt => opt.MapFrom(src => src.department.Name))
                .ForMember(des => des.InstructorName, opt => opt
                .MapFrom(src => src.CourseInstructors
                .Select(ci => ci.Instructor.Name).ToList()))
                .ForMember(des => des.Students, opt => opt
                .MapFrom(src => src.StudentCourses.Select(sc => sc.Student.Name).ToList()))
                .ForMember(des => des.studentWithGrades, opt => opt.MapFrom(src => src.StudentCourses.Select(sc => new StudentWithGrade
                { StudentName = sc.Student.Name, Grade = sc.grade }).ToList()));

            //**********************************************************
            //create maping between course and courseedit 

            CreateMap<Course, CourseEditViewModel>()
                .ForMember(des => des.StudentsItem, opt => opt.Ignore())
                .ForMember(des => des.DepartmentsItem, opt => opt.Ignore())
                .ForMember(des => des.InstructorsItem, opt => opt.Ignore())
                .ForMember(des => des.InstructorsId, opt => opt.MapFrom(src => src.CourseInstructors.Select(ci => ci.InstructorId).ToList()))
                .ForMember(des => des.StudentsId, opt => opt.MapFrom(src => src.StudentCourses.Select(sc => sc.StudentId).ToList()))
                .ForMember(des => des.StudentWithGrades, opt => opt.MapFrom(src => src.StudentCourses.Select(sc => new StudentWithGrade
                {   Grade = sc.grade,
                    StudentId = sc.StudentId,
                    StudentName = sc.Student.Name
                }
                ).ToList()));

            CreateMap<CourseEditViewModel, Course>()
                .ForMember(des => des.StudentCourses, opt => opt.Ignore())
                .ForMember(des => des.CourseInstructors, opt => opt.Ignore())
                .ForMember(des => des.department, opt => opt.Ignore());
        }
    }
}
