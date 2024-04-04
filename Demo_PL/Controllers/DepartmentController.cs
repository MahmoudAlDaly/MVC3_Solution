using Demo_BLL.Interfaces;
using Demo_DAL.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using System;

namespace Demo_PL.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly IDepartmentRepository Repository;
        private readonly IWebHostEnvironment Env;
        public DepartmentController(IDepartmentRepository repository, IWebHostEnvironment env)
        {
            Repository = repository;
            Env = env;
        }

        // /Department/index
        public IActionResult Index()
        {
            var dep = Repository.GetAll();
            return View(dep);
        }

        // /Department/Create
        [HttpGet]  //default
        public IActionResult Create()
        {
            return View();
        }

        // /Department/Create
        [HttpPost]  
        public IActionResult Create(Department department)
        {
            if (ModelState.IsValid)
            {
                var count = Repository.Add(department);
                if (count > 0)
                {
                    TempData["Message"] = "department is created";
                }
                else
                {
                    TempData["Message"] = "department is not created";
                }

                return RedirectToAction(nameof(Index));
            }

            return View(department);
        }

        // /Department/Details
        // /Department/Details/10
        [HttpGet]
        public IActionResult Details(int? id,string ViewName = "Details")
        {
            if (!id.HasValue)
            {
                return BadRequest(); //400
            }
            else
            {
                var department = Repository.Get(id.Value);

                if (department == null)
                {
                    return NotFound();    //404
                }
                return View(ViewName,department);
            }
        }


        [HttpGet]
        public IActionResult Edit(string ViewName,int? id)
        {
            return Details(id, "Edit");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit([FromRoute]int id,Department department)
        {
            if (id != department.ID)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return View(department);
            }

            try
            {
                Repository.Update(department);

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {

                if (Env.IsDevelopment())
                {
                    ModelState.AddModelError(string.Empty,ex.Message);
                }
               
                    ModelState.AddModelError(string.Empty, "updating Error");
                    return View(department);
                
            }

            
        }


        [HttpGet]
        public IActionResult Delete(int? id)
        {
            return Details(id, "Delete");
        }

        [HttpPost]
        public IActionResult Delete(Department department)
        {
            try
            {
                Repository.Delete(department);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                if (Env.IsDevelopment())
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                }

                ModelState.AddModelError(string.Empty, "Deleteing Error");
                return View(department);

            }
            
        }
    }
}
