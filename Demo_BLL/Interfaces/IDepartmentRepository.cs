using Demo_DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo_BLL.Interfaces
{
    public interface IDepartmentRepository
    {
        IEnumerable<Department> GetAll();
        Department Get(int id);
        int Add(Department Entity);
        int Update(Department Entity);
        int Delete(Department Entity);
    }
}
