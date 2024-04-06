using Demo_DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo_BLL.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        //public IEmployeeRepository UEmployeeRepository { get; set; }
        //public IGenericRepository<Department> UDepartmentRepository { get; set; }

        IGenericRepository<T> Urepository<T>() where T : ModelBase;

        int Complete();
    }
}
