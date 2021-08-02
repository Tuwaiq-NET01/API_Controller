using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TestAPI.Data;
using TestAPI.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TestAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : Controller
    {
        private readonly AppDbContext _db;

        public EmployeesController(AppDbContext db)
        {
            _db = db;
        }

        // GET - api/employees
        [HttpGet]
        public IActionResult Index()
        {
            //_logger.LogInfo("Inside the GetCompanies method.");

            var employees = _db.Employees.ToList();
            return Ok(employees);

        }

        // GET - api/employees/:id
        // GET - api/employees/7
        [HttpGet("{id}")]
        public IActionResult Details(int? id)
        {
            var employee = _db.Employees.ToList().Find(employee => employee.Id == id);

            if (id == null || employee == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(employee);
            }

        }


        [HttpPost]
        public IActionResult Create([FromBody] EmployeeModel employee)
        {
            if (ModelState.IsValid)
            {
                _db.Employees.Add(employee);
                _db.SaveChanges();

            }

            return Json(employee);

        }

        [HttpPut("{id}")]
        public IActionResult Edit(int? id, [FromBody]EmployeeModel employee)
        {
            //var oldEmployee = _db.Employees.ToList().Find(e => e.Id == id);
            if (id == null )
            {
                return NotFound();
            }
            employee.Id = (int)id;
            _db.Employees.Update(employee);
            _db.SaveChanges();

            return Ok(employee);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int? id)
        {
            var employee = _db.Employees.ToList().FirstOrDefault(e => e.Id == id);
            if (id == null || employee == null)
            {
                return NotFound();
            }
            _db.Employees.Remove(employee);
            _db.SaveChanges();

            return Ok(employee);
        }

    }


}
