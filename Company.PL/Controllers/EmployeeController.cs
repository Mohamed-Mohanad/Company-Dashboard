using AutoMapper;
using Company.BLL.Interfaces;
using Company.DAL.Entities;
using Company.PL.Helper;
using Company.PL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Company.PL.Controllers
{
    [Authorize]
    public class EmployeeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public EmployeeController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }


        public async Task<IActionResult> Index(string searchValue = "")
        {
            if(string.IsNullOrEmpty(searchValue))
            { 
                var employees = await _unitOfWork.EmployeeRepository.GetAll();
                var mappedEmployees = _mapper.Map<IEnumerable<EmployeeViewModel>>(employees);
                return View(mappedEmployees);
            }
            else
            {
                var employees = await _unitOfWork.EmployeeRepository.Search(searchValue);
                var mappedEmployees = _mapper.Map<IEnumerable<EmployeeViewModel>>(employees);
                return View(mappedEmployees);
            }
        }
        [Authorize(Roles = "Admin, HR")]
        public IActionResult Create()
        {
            //ViewBag.Departments = await _unitOfWork.DepartmentRepository.GetAll();
            return View();
        }
        [HttpPost]
        [Authorize(Roles = "Admin, HR")]
        public async Task<IActionResult> Create(EmployeeViewModel employee)
        {
            if(ModelState.IsValid)
            {
                employee.ImageName = DocumentSettings.UploadFile(employee.Image, "images");
                var mappEmployee = _mapper.Map<Employee>(employee);
                await _unitOfWork.EmployeeRepository.Create(mappEmployee);
                return RedirectToAction(nameof(Index));
            }
            //ViewBag.Departments = await _unitOfWork.DepartmentRepository.GetAll();
            return View(employee);
        }
        [Authorize(Roles = "Admin, HR")]
        public async Task<IActionResult> Update(int? id)
        {
            if (id == null)
                return NotFound();
            var employee = await _unitOfWork.EmployeeRepository.Get(id);
            if (employee == null)
                return NotFound();
            var mappedEmployee = _mapper.Map<EmployeeViewModel>(employee);
            //ViewBag.Department = await _unitOfWork.DepartmentRepository.GetAll();
            return View(mappedEmployee);
        }
        [HttpPost]
        [Authorize(Roles = "Admin, HR")]
        public async Task<IActionResult> Update(int? id, EmployeeViewModel employee)
        {
            if (id != employee.Id)
                return BadRequest();
            if(ModelState.IsValid)
            {
                try
                {
                    employee.ImageName = DocumentSettings.UploadFile(employee.Image!, "images");
                    var mappedEmp = _mapper.Map<Employee>(employee);
                    await _unitOfWork.EmployeeRepository.Update(mappedEmp);
                    return RedirectToAction(nameof(Index));
                }catch(Exception)
                {
                    return View(employee);
                }
            }
            //ViewBag.Department = await _unitOfWork.DepartmentRepository.GetAll();
            return View(employee);
        }
        [HttpGet]
        [Authorize(Roles = "Admin, HR")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();
            var employee = await _unitOfWork.EmployeeRepository.Get(id);
            if(employee == null)
                return NotFound();
            await _unitOfWork.EmployeeRepository.Delete(employee);
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();
            var employee = await _unitOfWork.EmployeeRepository.Get(id);
            if (employee == null)
                return NotFound();
            var mappedEmployee = _mapper.Map<EmployeeViewModel>(employee);
            return View(mappedEmployee);
        }
    }
}
