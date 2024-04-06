using Demo_BLL.Interfaces;
using Demo_DAL.Data;
using Demo_DAL.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo_BLL.Repositories
{
    public class UnitOfWork : IUnitOfWork 
    {
        private readonly ApplicationDbContext DbContext;
        private Hashtable Repositories;

        public IEmployeeRepository UEmployeeRepository { get ; set ; }
        public IGenericRepository<Department> UDepartmentRepository { get ; set; }

        public UnitOfWork(ApplicationDbContext dbContext)
        {
            DbContext = dbContext;
            UEmployeeRepository = new EmployeeRepository(DbContext);
            UDepartmentRepository = new DepartmentRepository(DbContext);
        }

        public int Complete()
        {
           return DbContext.SaveChanges();
        }

        public void Dispose()
        {
            DbContext.Dispose();
        }

        public IGenericRepository<T> Urepository<T>() where T : ModelBase
        {
            var key = typeof(T).Name;
            if (!Repositories.ContainsKey(key))
            {
                if (key == nameof(Employee))
                {
                    var rep = new EmployeeRepository(DbContext);
                    Repositories.Add(key, rep);
                }
                else
                {
                    var repo = new GenericRepository<T>(DbContext);
                    Repositories.Add(key, repo);
                }
                
            }
            return Repositories[key] as IGenericRepository<T>;
        }
    }
}
