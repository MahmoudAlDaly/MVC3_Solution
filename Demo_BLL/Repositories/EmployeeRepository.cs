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
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly ApplicationDbContext DbContext;


        public EmployeeRepository(ApplicationDbContext dbContext)
        {
            DbContext = dbContext;
        }
        public Employee Get(int id)
        {
            #region MyRegion
            //var department = DbContext.Departments.Local.Where(d => d.ID == id).FirstOrDefault();

            //if (department == null)
            //{
            //    DbContext.Departments.Where(d => d.ID == id).FirstOrDefault();
            //}

            //return department; 
            #endregion

            return DbContext.Employees.Find(id);

        }


        public IEnumerable<Employee> GetAll()
        {
            return DbContext.Employees.AsNoTracking().ToList();
        }

        public int Add(Employee Entity)
        {
            DbContext.Employees.Add(Entity);
            return DbContext.SaveChanges();
        }

        public int Update(Employee Entity)
        {
            DbContext.Employees.Update(Entity);
            return DbContext.SaveChanges();
        }

        public int Delete(Employee Entity)
        {
            DbContext.Employees.Remove(Entity);
            return DbContext.SaveChanges();
        }
    }
}
