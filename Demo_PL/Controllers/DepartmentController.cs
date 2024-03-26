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

        //Department/index
        public IActionResult Index()
        {
            return View();
        }
    }
}
