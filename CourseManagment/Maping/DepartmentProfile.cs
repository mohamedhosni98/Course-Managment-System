using AutoMapper;
using CourseManagment.Models;
using CourseManagment.ViewModel.DepartmentVM;

namespace CourseManagment.Maping
{
    public class DepartmentProfile:Profile
    {
        public DepartmentProfile()
        {
            CreateMap<DepartmentViewModel, Department>();
            CreateMap<Department,DepartmentViewModel>();
            CreateMap<Department, DepartmentViewModelWithIndex>()
                .ForMember(des => des.InstructorCount, opt => opt.MapFrom(src => src.instructors.Count))
                .ForMember(des => des.CourseCount, opt => opt.MapFrom(src => src.courses.Count))
                .ForMember(des => des.StudentCount, opt => opt.MapFrom(src => src.students.Count));

            CreateMap<DepartmentEditViewModel, Department>();
            CreateMap<Department,DepartmentEditViewModel>();

            CreateMap<DepartmentDeleteViewModel, Department>();
            CreateMap<Department, DepartmentDeleteViewModel>();

            CreateMap<Department, DepartmentDetailsViewModel>();
                
        }
    }
}
