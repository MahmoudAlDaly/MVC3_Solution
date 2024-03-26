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
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly ApplicationDbContext DbContext;


        public DepartmentRepository(ApplicationDbContext dbContext)
        {
            DbContext = dbContext;
        }
        public Department Get(int id)
        {
            //var department = DbContext.Departments.Local.Where(d => d.ID == id).FirstOrDefault();

            //if (department == null)
            //{
            //    DbContext.Departments.Where(d => d.ID == id).FirstOrDefault();
            //}

            //return department;

            return DbContext.Departments.Find(id);

        }


        public IEnumerable<Department> GetAll()
        {
            return DbContext.Departments.AsNoTracking().ToList();
        }

        public int Add(Department Entity)
        {
            DbContext.Departments.Add(Entity);
            return DbContext.SaveChanges();
        }

        public int Update(Department Entity)
        {
            DbContext.Departments.Update(Entity);
            return DbContext.SaveChanges();
        }

        public int Delete(Department Entity)
        {
            DbContext.Departments.Remove(Entity);
            return DbContext.SaveChanges();
        }


        
    }
}
