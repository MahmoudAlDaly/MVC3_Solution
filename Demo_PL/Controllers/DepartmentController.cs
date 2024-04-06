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
        private readonly IUnitOfWork UnitOfWork;

        //private readonly IDepartmentRepository Repository;
        private readonly IWebHostEnvironment Env;
        public DepartmentController(IUnitOfWork unitOfWork, IWebHostEnvironment env)
        {
            //Repository = repository;
            UnitOfWork = unitOfWork;
            Env = env;
        }

        // /Department/index
        public IActionResult Index()
        {
            var dep = UnitOfWork.Urepository<Department>().GetAll();
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
                UnitOfWork.Urepository<Department>().Add(department);
                var count = UnitOfWork.Complete();
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
                var department = UnitOfWork.Urepository<Department>().Get(id.Value);

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
                UnitOfWork.Urepository<Department>().Update(department);
                UnitOfWork.Complete();
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
                UnitOfWork.Urepository<Department>().Delete(department);
                UnitOfWork.Complete();
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
