using Demo_BLL.Interfaces;
using Demo_DAL.Models;
using Microsoft.AspNetCore.Mvc;

namespace Demo_PL.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly IDepartmentRepository Repository;

        public DepartmentController(IDepartmentRepository repository)
        {
            Repository = repository;
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
                    return RedirectToAction(nameof(Index));
                }
            }

            return View(department);
        }

        // /Department/Details
        // /Department/Details/10
        public IActionResult Details(int? id)
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
                return View(department);
            }
        }
    }
}
