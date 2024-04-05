using Demo_BLL.Interfaces;
using Demo_DAL.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Demo_PL.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IEmployeeRepository Repo_Employee;
        //private readonly IDepartmentRepository Repo_Department;
        private readonly IWebHostEnvironment Env;
        public EmployeeController(IEmployeeRepository repo_employee, IWebHostEnvironment env)
        {
            Repo_Employee = repo_employee;
            //Repo_Department = repo_department;
            Env = env;
        }


        // /Employee/Index
        //[HttpGet]
        public IActionResult Index(string serachinput)
        {
            // 1- viewdata
            ViewData["Message"] = "Hello ViewData";

            // 2- viewbag
            ViewBag.message = "Hi ViewBag";

            var emp = Enumerable.Empty<Employee>();

            if (string.IsNullOrEmpty(serachinput))
            {
                emp = Repo_Employee.GetAll();
            }
            else
            {
                emp = Repo_Employee.GetEmployeesByName(serachinput.ToLower());
            }

             
            return View(emp);
        }

        public IActionResult Create()
        {
            //ViewData["Departments"] = Repo_Department.GetAll();
            return View();
        }

        [HttpPost]
        public IActionResult Create(Employee employee)
        {
            if (ModelState.IsValid)
            {
                var count = Repo_Employee.Add(employee);
                if (count > 0)
                {
                    TempData["Message"] = "Employee is created";
                }
                else
                {
                    TempData["Message"] = "Employee is not created";
                }

                return RedirectToAction(nameof(Index));
            }

            return View(employee);
        }

        // /employee/Details
        // /employee/Details/10
        [HttpGet]
        public IActionResult Details(int? id, string ViewName = "Details")
        {
            if (!id.HasValue)
            {
                return BadRequest(); //400
            }
            else
            {
                var employee = Repo_Employee.Get(id.Value);

                if (employee == null)
                {
                    return NotFound();    //404
                }
                return View(ViewName, employee);
            }
        }


        [HttpGet]
        public IActionResult Edit(string ViewName, int? id)
        {
            //ViewData["Departments"] = Repo_Department.GetAll();

            return Details(id, "Edit");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit([FromRoute] int id, Employee employee)
        {
            if (id != employee.ID)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return View(employee);
            }

            try
            {
                Repo_Employee.Update(employee);

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {

                if (Env.IsDevelopment())
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                }

                ModelState.AddModelError(string.Empty, "updating Error");
                return View(employee);

            }


        }


        [HttpGet]
        public IActionResult Delete(int? id)
        {
            return Details(id, "Delete");
        }

        [HttpPost]
        public IActionResult Delete(Employee employee)
        {
            try
            {
                Repo_Employee.Delete(employee);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                if (Env.IsDevelopment())
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                }

                ModelState.AddModelError(string.Empty, "Deleteing Error");
                return View(employee);

            }

        }
    }
}
