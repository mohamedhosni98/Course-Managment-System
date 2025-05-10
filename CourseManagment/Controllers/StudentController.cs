using AutoMapper;
using CourseManagment.Models;
using CourseManagment.Repository;
using CourseManagment.Repository.RepoManager;
using CourseManagment.ViewModel.StudentVM;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CourseManagment.Controllers
{
    [Authorize]
    public class StudentController : Controller
    {
        private readonly IStudentRepository studentRepository;
        private readonly IMapper mapper;
        private readonly IDepartmentRepository departmentRepository;
        private readonly IWebHostEnvironment webHostEnvironment;

        public StudentController(IStudentRepository studentRepository
            , IMapper mapper
            , IDepartmentRepository departmentRepository,
            IWebHostEnvironment webHostEnvironment)
        {
            this.studentRepository = studentRepository;
            this.mapper = mapper;
            this.departmentRepository = departmentRepository;
            this.webHostEnvironment = webHostEnvironment;
        }

        // display all student 
        public IActionResult Index()
        {
            var students = studentRepository.Index();
            var model = mapper.Map<List<StudentIndexViewModel>>(students);
            return View(model);
        }

        // add new student 
        [HttpGet]
        public IActionResult Create()
        {
            var viewmodel = new StudentAddViewModel()
            {
                DepartmentName = departmentRepository.Index()
             .Select(d => new SelectListItem
             {
                 Value = d.Id.ToString()
             ,
                 Text = d.Name
             })
            };
            return View(viewmodel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(StudentAddViewModel model)
        {
            if (ModelState.IsValid)
            {
                var student = mapper.Map<Student>(model);
                if (model.ImageFile != null && model.ImageFile.Length > 0)
                {
                    var extension = Path.GetExtension(model.ImageFile.FileName).ToLowerInvariant();
                    var filename = Guid.NewGuid().ToString() + extension;
                    var filePath = Path.Combine(webHostEnvironment.WebRootPath, "images", filename);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        model.ImageFile.CopyTo(stream);
                    }

                    student.Image = $"/images/{filename}"; 
                }
                studentRepository.Create(student);
                return RedirectToAction("Index");
            }

            model.DepartmentName = departmentRepository.Index()
                .Select(d => new SelectListItem
                { Value = d.Id.ToString()
                ,
                    Text = d.Name });

            return View(model);


        }

        [HttpGet]
        public IActionResult Details(int id)
        {
            var student = studentRepository.GetStudent(id);
           var model =mapper.Map<StudentDetailsViewModel>(student);

            return View(model);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var student = studentRepository.GetStudent(id);
            var model = mapper.Map<StudentEditViewModel>(student);
            model.Departments = departmentRepository.Index().Select(d => new SelectListItem
            {
                Value = d.Id.ToString()
            ,
                Text = d.Name
            });
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task< IActionResult> Edit(int id,StudentEditViewModel viewModel )
        {
            if (ModelState.IsValid)
            {
                var student = studentRepository.GetStudent(id);
                if (viewModel.ImageFile != null && viewModel.ImageFile.Length > 0)
                {
                    string uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images");
                    string uniqueFileName = Guid.NewGuid().ToString() + "_" + viewModel.ImageFile.FileName;
                    string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await viewModel.ImageFile.CopyToAsync(fileStream);
                    }

                    viewModel.Image = "/images/" + uniqueFileName;
                }
                else
                {
                    viewModel.Image = student.Image; // احتفظ بالصورة القديمة
                }

                // تحديث البيانات
                var updatestudent = mapper.Map(viewModel, student);
                studentRepository.Update(id, updatestudent);

                return RedirectToAction(nameof(Index));
            }
            viewModel.Departments = departmentRepository.Index()
                .Select(d => new SelectListItem { Value = d.Id.ToString(), Text = d.Name });
            return View(viewModel);

        }


        // delete student 
        public IActionResult Delete(int id)
        {
            var student = studentRepository.GetStudent(id);
           var studentmodel=  mapper.Map<StudentDetailsViewModel>(student);
            return View(studentmodel);
        }

        [HttpPost,ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult Deleteconfirmed(int id)
        {
          
            studentRepository.Delete(id);
            return RedirectToAction("index"); 
            
        }
    
        
           
        }
}
