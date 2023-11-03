using EmployeeManagement_Demo.Models;
using EmployeeManagement_Demo.Services;
using EmployeeManagement_Demo.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagement_Demo.Controllers
{
    [Authorize]
    public class EmployeeController : Controller
    {
        private readonly IEmployeeReprository employeeReprository;

        public EmployeeController(IEmployeeReprository employeeReprository)
        {
            this.employeeReprository = employeeReprository;
        }

        [AllowAnonymous]
        public IActionResult Index()
        {
            var model = employeeReprository.GetEmployees();
            return View(model);
        }

        [AllowAnonymous]
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            var model = employeeReprository.GetEmployeeByID(id);
            return View(model);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(EmployeeCreateViewModel emp)
        {
            if (emp == null)
            {
                return BadRequest();
            }
            if(ModelState.IsValid)
            {
                employeeReprository.AddEmployee(emp);
                return RedirectToAction(nameof(Index));
            }
            
            return View();
        }

        
        public IActionResult Delete(int? id)
        {
            if(id == null)
            {
                return BadRequest();
            }

            employeeReprository.DeleteEmployee(id);
            return RedirectToAction(nameof(Index));
        }

        
        public IActionResult Edit(int? id)
        {
            if(id == null)
            {
                return BadRequest();
            }

            var employee = employeeReprository.GetEmployeeByID(id);
            if(employee == null)
                return NotFound();
            EmployeeEditViewModel model = new EmployeeEditViewModel()
            {
                id = employee.Id,
                Name = employee.Name,
                Department = employee.Department,
                Email = employee.Email,
                existedPhoto = employee.PhotoPath
                    
            };
            return View(model);
        }

        [HttpPost]
        
        public IActionResult Edit(int? id,  EmployeeEditViewModel model)
        {
            if(id==null)
                return BadRequest();
            if(ModelState.IsValid)
            {
                employeeReprository.UpdateEmployee(id, model);
                return RedirectToAction(nameof(Details), new {id });
            }
            return View(model);
        }

    }
}
