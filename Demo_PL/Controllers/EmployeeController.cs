using AutoMapper;
using Demo_BLL.Interfaces;
using Demo_BLL.Repositories;
using Demo_DAL.Models;
using Demo_PL.Helpers;
using Demo_PL.ViewModels;
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
        private readonly IUnitOfWork UnitOfWork;

        //private readonly IEmployeeRepository Repo_Employee;
        //private readonly IDepartmentRepository Repo_Department;
        private readonly IMapper Mapper;
        private readonly IWebHostEnvironment Env;


        public EmployeeController(IUnitOfWork unitOfWork,IMapper mapper, IWebHostEnvironment env)
        {
            
            //Repo_Department = repo_department;
            //Repo_Employee = repo_employee;
            UnitOfWork = unitOfWork;
            Mapper = mapper;
            Env = env;
        }


        // /Employee/Index
        //[HttpGet]
        public IActionResult Index(string serachinput)
        {
            // 1- viewdata
            //ViewData["Message"] = "Hello ViewData";

            // 2- viewbag
            //ViewBag.message = "Hi ViewBag";

            var emp = Enumerable.Empty<Employee>();

            var emp_repo = UnitOfWork.Urepository<Employee>() as EmployeeRepository;

            if (string.IsNullOrEmpty(serachinput))
            {
                emp = emp_repo.GetAll();
            }
            else
            {
                emp = emp_repo.GetEmployeesByName(serachinput.ToLower());
            }

             var mappedEmp = Mapper.Map<IEnumerable<Employee>,IEnumerable<EmployeeViewModel>>(emp);

            return View(mappedEmp);
        }

        public IActionResult Create()
        {
            //ViewData["Departments"] = Repo_Department.GetAll();
            return View();
        }

        [HttpPost]
        public IActionResult Create(EmployeeViewModel employeeVM)
        {
            if (ModelState.IsValid)
            {
                employeeVM.ImageName = DocumentSettings.UploadFile(employeeVM.image, "Images");

                var mappedEmp = Mapper.Map<EmployeeViewModel,Employee>(employeeVM);

                UnitOfWork.Urepository<Employee>().Add(mappedEmp);

                var count = UnitOfWork.Complete();

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

            return View(employeeVM);
        }

        // /employee/Details
        // /employee/Details/10
        //[HttpGet]
        public IActionResult Details(int? id, string ViewName = "Details")
        {
            if (!id.HasValue)
            {
                return BadRequest(); //400
            }
            else
            {
                var employee = UnitOfWork.Urepository<Employee>().Get(id.Value);

                var mappedEmp = Mapper.Map<Employee, EmplyeeResponseViewModel>(employee);


                if (employee == null)
                {
                    return NotFound();    //404
                }
                if (ViewName.Equals("Delete",StringComparison.OrdinalIgnoreCase))
                {
                    TempData["ImageName"] = employee.ImageName;
                }
                return View(ViewName, mappedEmp);
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
        public IActionResult Edit([FromRoute] int id, EmployeeViewModel employeeVM)
        {
            if (id != employeeVM.ID)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return View(employeeVM);
            }

            try
            {
                var mappedEmp = Mapper.Map<EmployeeViewModel, Employee>(employeeVM);
                UnitOfWork.Urepository<Employee>().Update(mappedEmp);
                UnitOfWork.Complete();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {

                if (Env.IsDevelopment())
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                }

                ModelState.AddModelError(string.Empty, "updating Error");
                return View(employeeVM);

            }


        }


        [HttpGet]
        public IActionResult Delete(int? id)
        {
            return Details(id, "Delete");
        }

        [HttpPost]
        public IActionResult Delete(EmplyeeResponseViewModel employeeVM)
        {
            try
            {
                employeeVM.ImageName = TempData["ImageName"] as string;
                var mappedEmp = Mapper.Map<EmplyeeResponseViewModel, Employee>(employeeVM);
                UnitOfWork.Urepository<Employee>().Delete(mappedEmp);
                int count =UnitOfWork.Complete();
                if (count > 0)
                {
                    DocumentSettings.DeleteFile(employeeVM.ImageName,"images");
                    return RedirectToAction(nameof(Index)); 
                }
                return View(employeeVM);

            }
            catch (Exception ex)
            {
                if (Env.IsDevelopment())
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                }

                ModelState.AddModelError(string.Empty, "Deleteing Error");
                return View(employeeVM);

            }

        }
    }
}
