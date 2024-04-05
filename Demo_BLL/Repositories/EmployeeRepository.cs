using Demo_BLL.Interfaces;
using Demo_DAL.Data;
using Demo_DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo_BLL.Repositories
{
    public class EmployeeRepository : GenericRepository<Employee>, IEmployeeRepository
    {
        //private readonly ApplicationDbContext DbContext;

        public EmployeeRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            //DbContext = dbContext;
        }

        public IQueryable<Employee> GetEmployeesByAddress(string address)
        {
            return DbContext.Employees.Where(e => e.Address.ToLower() == address.ToLower());
        }

        public IQueryable<Employee> GetEmployeesByName(string name)
        {
            return DbContext.Employees.Where(e => e.Name.ToLower().Contains(name));
        }
    }
}
