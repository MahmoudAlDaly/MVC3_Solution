using Demo_DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo_BLL.Interfaces
{
    public interface IGenericRepository<T> where T : ModelBase
    {
        Task <IEnumerable<T>> GetAllAsync();

        Task<T> GetAsync(int id);
        void Add(T Entity);
        void Update(T Entity);
        void Delete(T Entity);
    }
}
