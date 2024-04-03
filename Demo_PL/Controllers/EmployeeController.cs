using Demo_BLL.Interfaces;
using Demo_DAL.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using System;

namespace Demo_PL.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IEmployeeRepository Repository;
        private readonly IWebHostEnvironment Env;
        public EmployeeController(IEmployeeRepository repository, IWebHostEnvironment env)
        {
            Repository = repository;
            Env = env;
        }


        // /Employee/Index
        [HttpGet]
        public IActionResult Index()
        {
            // 1- viewdata
            ViewData["Message"] = "Hello ViewData";

            // 2- viewbag
            ViewBag.message = "Hi ViewBag";

            var emp = Repository.GetAll();
            return View(emp);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Employee employee)
        {
            if (ModelState.IsValid)
            {
                var count = Repository.Add(employee);
                if (count > 0)
                {
                    return RedirectToAction(nameof(Index));
                }
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
                var employee = Repository.Get(id.Value);

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
                Repository.Update(employee);

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
                Repository.Delete(employee);
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
