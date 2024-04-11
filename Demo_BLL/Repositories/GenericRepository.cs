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


        public async Task<T> GetAsync(int id)
        {
            #region MyRegion
            //var department = DbContext.Departments.Local.Where(d => d.ID == id).FirstOrDefault();

            //if (department == null)
            //{
            //    DbContext.Departments.Where(d => d.ID == id).FirstOrDefault();
            //}

            //return department; 
            #endregion

            return await DbContext.FindAsync<T>(id);

        }


        public virtual async Task <IEnumerable<T>> GetAllAsync()
        {
            if (typeof(T) == typeof(Employee))
            {
                return (IEnumerable<T>) DbContext.Employees.Include(e=> e.Department_Nav).AsNoTracking().ToListAsync();
            }
            else
            {
                return await DbContext.Set<T>().AsNoTracking().ToListAsync();
            }
            
        }

        public void Add(T Entity)
        {
            DbContext.Set<T>().Add(Entity);
            //return DbContext.SaveChanges();
        }

        public void Update(T Entity)
        {
            DbContext.Set<T>().Update(Entity);
            //return DbContext.SaveChanges();
        }

        public void Delete(T Entity)
        {
            DbContext.Set<T>().Remove(Entity);
            //return DbContext.SaveChanges();
        }
    }
}
