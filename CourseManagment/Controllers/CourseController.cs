using AutoMapper;
using CourseManagment.Models;
using CourseManagment.Repository;
using CourseManagment.Repository.RepoManager;
using CourseManagment.ViewModel.CourseVM;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CourseManagment.Controllers
{
    [Authorize]
    public class CourseController : Controller
    {
        private readonly IMapper mapper;
        private readonly ICourseRepository courseRepository;
        private readonly IDepartmentRepository departmentRepository;
        private readonly IInstructorRepository instructorRepository;
        private readonly IStudentRepository studentRepository;

        public CourseController(IMapper mapper, ICourseRepository courseRepository
            , IDepartmentRepository departmentRepository
            , IInstructorRepository instructorRepository, IStudentRepository studentRepository)
        {
            this.mapper = mapper;
            this.courseRepository = courseRepository;
            this.departmentRepository = departmentRepository;
            this.instructorRepository = instructorRepository;
            this.studentRepository = studentRepository;
        }
        [HttpGet]
        public IActionResult Index()
        {
            var courses = courseRepository.Index();

            var model = mapper.Map<List<CourseIndexViewModel>>(courses);
            return View(model);
        }

        [HttpGet]
        public IActionResult Create()
        {
            var model = new CourseAddViewModel()
            {
                Departments = departmentRepository.Index().Select(d => new SelectListItem
                {
                    Value = d.Id.ToString()
                ,
                    Text = d.Name
                })
                ,
                Instructors = instructorRepository.Index().Select(i => new SelectListItem
                {
                    Value = i.Id.ToString()
                ,
                    Text = i.Name
                }),
                Students = studentRepository.Index().Select(s => new SelectListItem
                {
                    Value = s.Id.ToString()
                    ,
                    Text = s.Name
                }),
                SelectedInstructorIds = new List<int>(),
                SelectedStudentIds = new List<int>()

            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CourseAddViewModel model)
        {
            if (ModelState.IsValid)
            {
                var modelvm = mapper.Map<Course>(model);
                courseRepository.Create(modelvm);
                return RedirectToAction("Index");

            }
            model.Departments = departmentRepository.Index()
                .Select(d => new SelectListItem { Value = d.Id.ToString(), Text = d.Name })
                .ToList();
            model.Instructors = instructorRepository.Index()
                .Select(i => new SelectListItem { Value = i.Id.ToString(), Text = i.Name })
                .ToList();
            model.Students = studentRepository.Index()
                .Select(s => new SelectListItem { Value = s.Id.ToString(), Text = s.Name })
                .ToList();

            return View(model);
        }

        [HttpGet]
        public IActionResult Details(int id)
        {
            var course = courseRepository.GetCourse(id);
            var coursemodel = mapper.Map<CourseDetailsViewModel>(course);

            return View(coursemodel);
        }


        // course Edit 
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var course = courseRepository.GetCourse(id);
            if (course == null)
                return NotFound();

            var model = mapper.Map<CourseEditViewModel>(course);

            model.DepartmentsItem = departmentRepository.Index()
                .Select(d => new SelectListItem { Value = d.Id.ToString(), Text = d.Name })
                .ToList();

            model.InstructorsItem = instructorRepository.Index()
                .Select(i => new SelectListItem { Value = i.Id.ToString(), Text = i.Name })
                .ToList();

            model.StudentsItem = studentRepository.Index()
                .Select(s => new SelectListItem { Value = s.Id.ToString(), Text = s.Name })
                .ToList();

            model.InstructorsId = course.CourseInstructors.Select(ci => ci.InstructorId).ToList();
            model.StudentsId = course.StudentCourses.Select(sc => sc.StudentId).ToList();
            model.StudentWithGrades = course.StudentCourses
                .Select(st => new StudentWithGrade { StudentId = st.StudentId, Grade = st.grade, StudentName = st.Student.Name })
                .ToList();

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, CourseEditViewModel model)
        {
            if (id != model.Id)
                return BadRequest("Course ID mismatch.");

            if (ModelState.IsValid)
            {
                var course = courseRepository.GetCourse(id);
                if (course == null)
                    return NotFound();

                try
                {
                    mapper.Map(model, course);
                    course.DepartmentId = model.DepartmentId;

                    // تحقق من صحة InstructorsId
                    if (model.InstructorsId != null)
                    {
                        var validInstructorIds = instructorRepository.Index().Select(i => i.Id).ToList();
                        var invalidIds = model.InstructorsId.Except(validInstructorIds).ToList();
                        if (invalidIds.Any())
                        {
                            ModelState.AddModelError("InstructorsId", "One or more selected instructors are invalid.");
                            return View(model);
                        }
                    }

                    // تحديث المدربين
                    course.CourseInstructors.Clear();
                    if (model.InstructorsId != null)
                    {
                        foreach (var instructorId in model.InstructorsId.Distinct())
                        {
                            course.CourseInstructors.Add(new CourseInstructor
                            {
                                InstructorId = instructorId,
                                CourseId = course.Id
                            });
                        }
                    }

                    // تحديث الطلاب بناءً على StudentWithGrades
                    course.StudentCourses.Clear();
                    if (model.StudentWithGrades != null)
                    {
                        // تحقق من صحة StudentIds
                        var validStudentIds = studentRepository.Index().Select(s => s.Id).ToList();
                        var invalidStudentIds = model.StudentWithGrades
                            .Select(sg => sg.StudentId)
                            .Except(validStudentIds)
                            .ToList();
                        if (invalidStudentIds.Any())
                        {
                            ModelState.AddModelError("StudentWithGrades", "One or more selected students are invalid.");
                            return View(model);
                        }

                        foreach (var studentGrade in model.StudentWithGrades)
                        {
                            course.StudentCourses.Add(new StudentCourse
                            {
                                StudentId = studentGrade.StudentId,
                                CourseId = course.Id,
                                grade = studentGrade.Grade
                            });
                        }
                    }

                    courseRepository.Update(id, course);
                    return RedirectToAction("Index");
                }
                catch (AutoMapperMappingException ex)
                {
                    ModelState.AddModelError("", "Error mapping course data.");
                }
            }

            // إعادة تعبئة القوائم في حالة الفشل
            model.DepartmentsItem = departmentRepository.Index()
                .Select(d => new SelectListItem { Value = d.Id.ToString(), Text = d.Name })
                .ToList();

            model.InstructorsItem = instructorRepository.Index()
                .Select(i => new SelectListItem { Value = i.Id.ToString(), Text = i.Name })
                .ToList();

            model.StudentsItem = studentRepository.Index()
                .Select(s => new SelectListItem { Value = s.Id.ToString(), Text = s.Name })
                .ToList();

            model.InstructorsId = model.InstructorsId ?? new List<int>();
            model.StudentsId = model.StudentWithGrades?.Select(sg => sg.StudentId).ToList() ?? new List<int>();
            model.StudentWithGrades = model.StudentWithGrades ?? new List<StudentWithGrade>();

            return View(model);
        }

    

    }
        
        }

