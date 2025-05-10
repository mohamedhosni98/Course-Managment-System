using AutoMapper;
using CourseManagment.Models;
using CourseManagment.Repository.RepoManager;
using CourseManagment.ViewModel.DepartmentVM;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CourseManagment.Controllers
{
    [Authorize]
    public class DepartmentController : Controller
    {
        private readonly IDepartmentRepository departmentRepository;
        private readonly IMapper mapper;

        public DepartmentController(IDepartmentRepository departmentRepository,IMapper mapper ) //INJECTION 
        {
            this.departmentRepository = departmentRepository;
            this.mapper = mapper;
        }
        public IActionResult Index()   // INDEX DISPLAY ALL DEPARTMENT
        {
            var departments = departmentRepository.Index();
            var viewmodel=mapper.Map<List<DepartmentViewModelWithIndex>>(departments);  // USING AUTOMAPPER 
            
            return View(viewmodel); 
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View(); 
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(DepartmentViewModel model)
        {
            if (ModelState.IsValid)
            {
                var department = mapper.Map<Department>(model);
                departmentRepository.Create(department);
                return RedirectToAction("Index");
            }

            return View(model);
        }



        [HttpGet]
        public IActionResult Edit(int id)
        {
            var department =departmentRepository.GetDepartment(id);
            if (department == null )
                return NotFound();
            var model = mapper.Map<DepartmentEditViewModel>(department);
            return View(model); 
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, DepartmentEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                var dept  =mapper.Map<Department>(model);
                dept.Id = id;
                departmentRepository.Update(id, dept);
                return RedirectToAction("Index");
               
            }
            return View(model);
        }

         [HttpGet]
         public IActionResult Delete(int id)
         {
             var department = departmentRepository.GetDepartment(id);
             if (department == null)
                 return NotFound();
        
            var model= mapper.Map<DepartmentDeleteViewModel>(department);
             return View(model);
             
         }
         [HttpPost,ActionName("Delete")]
         [ValidateAntiForgeryToken]
         public IActionResult DeleteConfirmed(int id)
         {
                 departmentRepository.Delete(id);
        
                 return RedirectToAction("Index");
         }
        [HttpGet]
        public IActionResult Details(int id)
        {
            var department = departmentRepository.GetDepartment(id);
            var model=  mapper.Map<DepartmentDetailsViewModel>(department);
            return View(model); 
        }

       
       
    }
}
