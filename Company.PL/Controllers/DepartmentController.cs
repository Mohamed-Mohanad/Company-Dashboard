using Company.BLL.Interfaces;
using Company.DAL.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Company.PL.Controllers
{
    [Authorize]
    public class DepartmentController : Controller
    {
        private readonly IDepartmentRepository _repository;

        public DepartmentController(IDepartmentRepository repository)
        {
            _repository = repository;
        }
        public async Task<IActionResult> Index()
        {
            var data = await _repository.GetAll();
            return View(data);
        }
        public async Task<IActionResult> Details(int? id)
        { 
            if(id == null)
            {
                return NotFound();
            }
            var departmentData = await _repository.Get(id);
            if(departmentData == null)
            {
                return NotFound();
            }
            return View(departmentData);
        }
        [HttpGet]
        [Authorize(Roles = "Admin, HR")]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Department department)
        {
            if (ModelState.IsValid)
            {
                await _repository.Create(department);
                return RedirectToAction(nameof(Index));
            }
            return View(department);
        }
        [HttpGet]
        [Authorize(Roles = "Admin, HR")]
        public async Task<IActionResult> Update(int? id)
        {
            if (id == null)
                return NotFound();
            var departmentData = await _repository.Get(id);
            if(departmentData == null)
                return NotFound();
            return View(departmentData);
        }
        [HttpPost]
        [Authorize(Roles = "Admin, HR")]
        public async Task<IActionResult> Update(int id, Department department)
        {
            if (id != department.Id)
                return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    await _repository.Update(department);
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception)
                {
                    return View(department);
                }
                
            }
            return View(department);
        }
        [HttpGet]
        [Authorize(Roles = "Admin, HR")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();
            var departmentData = await _repository.Get(id);
            if (departmentData == null)
                return NotFound();
            return View(departmentData);
        }
        [HttpPost]
        [Authorize(Roles = "Admin, HR")]
        public async Task<IActionResult> Delete(int id, Department department)
        {
            if (id != department.Id)
                return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    await _repository.Delete(department);
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception)
                {
                    return View(department);
                }

            }
            return View(department);
        }
    }
}
