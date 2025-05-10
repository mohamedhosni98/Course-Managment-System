using AutoMapper;
using CourseManagment.Models;
using CourseManagment.ViewModel.InstructorVM;

namespace CourseManagment.Maping
{
    public class InstructorProfile: Profile
    {
        public InstructorProfile()
        {
            CreateMap<Instructor, InstructorAddViewModel>();
            CreateMap<InstructorAddViewModel,Instructor>()
                .ForMember(des=>des.DepartmentId, opt=>opt.MapFrom(src=>src.DepartmentId))
                .ForMember(des=>des.department,opt=>opt.Ignore())
                .ForMember(des=>des.Image,opt=>opt.MapFrom(src=>src.Image))
                .ForMember(des=>des.CourseInstructors,opt=>opt.Ignore());

            CreateMap<Instructor, InstructorIndexViewModel>();

            CreateMap<Instructor, InstructorDetailsViewModel>();

            CreateMap<Instructor, InstructorEditViewModel>();
            CreateMap<InstructorEditViewModel, Instructor>()
                .ForMember(des => des.department, opt => opt.Ignore())
                .ForMember(des=>des.CourseInstructors,opt=>opt.Ignore());
            CreateMap<InstructorAddViewModel, Instructor>();
        }
    }
}
