using AutoMapper;
using CourseManagment.Models;
using CourseManagment.Repository.RepoManager;
using CourseManagment.ViewModel.InstructorVM;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Identity.Client;

namespace CourseManagment.Controllers
{
    [Authorize]
    public class InstructorController : Controller
    {
        private readonly IInstructorRepository instructorRepository;
        private readonly IMapper mapper;
        private readonly IDepartmentRepository departmentRepository;

        public InstructorController(IInstructorRepository instructorRepository, IMapper mapper, IDepartmentRepository departmentRepository)
        {
            this.instructorRepository = instructorRepository;
            this.mapper = mapper;
            this.departmentRepository = departmentRepository;
        }

        [HttpGet]
        public IActionResult Index()  //display all instrutor
        {
            var instrutor = instructorRepository.Index();
            var instructor = mapper.Map<List<InstructorIndexViewModel>>(instrutor);
            return View(instructor);
        }

        // add new instructor 
        [HttpGet]
        public IActionResult Create()
        {
            var model = new InstructorAddViewModel()
            {

                Departments = departmentRepository.Index()   //fill department list 
                .Select(d => new SelectListItem
                {
                    Value = d.Id.ToString()
                ,
                    Text = d.Name
                })
            };
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(InstructorAddViewModel model)
        {
            if (ModelState.IsValid)
            {

                // حدد مسار الحفظ
                string uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images");
                string uniqueFileName = Guid.NewGuid().ToString() + "_" + model.ImageFile.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                // احفظ الصورة فعليًا
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await model.ImageFile.CopyToAsync(fileStream);
                }

                // خزّن اسم الصورة أو المسار في الداتابيز
                model.Image = "/images/" + uniqueFileName;
                var instructor = mapper.Map<Instructor>(model);
                instructorRepository.Create(instructor);
                return RedirectToAction("Index");

            }
            model.Departments = departmentRepository.Index()  // if model state is failed return fill department list 
                .Select(d => new SelectListItem
                {
                    Value = d.Id.ToString()
                ,
                    Text = d.Name
                });
            return View(model);
        }

        // display details for instructor  
        [HttpGet]
        public IActionResult Details(int id)
        {
            var instructor = instructorRepository.GetInstructor(id);
            var model = mapper.Map<InstructorDetailsViewModel>(instructor);
            return View(model);
        }

        // Edit 
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var instructor = instructorRepository.GetInstructor(id);
            if (instructor == null)
                return NotFound();
            var model = mapper.Map<InstructorEditViewModel>(instructor);
            model.Departments = departmentRepository.Index()
                .Select(d => new SelectListItem
                {
                    Value = d.Id.ToString()
                ,
                    Text = d.Name,
                    Selected = d.Id == model.DepartmentId
                });
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, InstructorEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                var instructor = instructorRepository.GetInstructor(id);
                if (instructor == null)
                    return NotFound();

                // التعامل مع الصورة
                if (model.ImageFile != null && model.ImageFile.Length > 0)
                {
                    string uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images");
                    string uniqueFileName = Guid.NewGuid().ToString() + "_" + model.ImageFile.FileName;
                    string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await model.ImageFile.CopyToAsync(fileStream);
                    }

                    model.Image = "/images/" + uniqueFileName;
                }
                else
                {
                    model.Image = instructor.Image; // احتفظ بالصورة القديمة
                }

                // تحديث البيانات
                var updatedInstructor = mapper.Map(model, instructor);
                instructorRepository.Update(id, updatedInstructor);

                return RedirectToAction(nameof(Index));
            }

            // إعادة تعبئة الأقسام في حالة الخطأ
            model.Departments = departmentRepository.Index()
                .Select(d => new SelectListItem
                {
                    Value = d.Id.ToString(),
                    Text = d.Name,
                    Selected = d.Id == model.DepartmentId
                });

            return View(model);
        }
        [HttpGet]
        public IActionResult Delete(int id )
        {
            var instructor = instructorRepository.GetInstructor(id);
            if (instructor == null)
                return NotFound();
           var instructormodel= mapper.Map<InstructorDetailsViewModel>(instructor);
            return View(instructormodel); 
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DelteConfirmed(int id)
        {
            instructorRepository.Delete(id);
            return RedirectToAction("index"); 
        }



    }
}

