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
    public class GenericRepository<T> : IGenericRepository<T> where T : ModelBase
    {
        private protected readonly ApplicationDbContext DbContext;


        public GenericRepository(ApplicationDbContext dbContext)
        {
            DbContext = dbContext;
        }
        public T Get(int id)
        {
            #region MyRegion
            //var department = DbContext.Departments.Local.Where(d => d.ID == id).FirstOrDefault();

            //if (department == null)
            //{
            //    DbContext.Departments.Where(d => d.ID == id).FirstOrDefault();
            //}

            //return department; 
            #endregion

            return DbContext.Set<T>().Find(id);

        }


        public IEnumerable<T> GetAll()
        {
            if (typeof(T) == typeof(Employee))
            {
                return (IEnumerable<T>) DbContext.Employees.Include(e=> e.Department_Nav).AsNoTracking().ToList();
            }
            else
            {
                return DbContext.Set<T>().AsNoTracking().ToList();
            }
            
        }

        public int Add(T Entity)
        {
            DbContext.Set<T>().Add(Entity);
            return DbContext.SaveChanges();
        }

        public int Update(T Entity)
        {
            DbContext.Set<T>().Update(Entity);
            return DbContext.SaveChanges();
        }

        public int Delete(T Entity)
        {
            DbContext.Set<T>().Remove(Entity);
            return DbContext.SaveChanges();
        }
    }
}
