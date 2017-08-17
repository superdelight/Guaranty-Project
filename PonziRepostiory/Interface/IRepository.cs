using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PonziRepostiory.Interface
{
    
    public interface IRepository<T> where T : class
    {
         bool Add(T Entry);
         bool AddRange(IEnumerable<T> Entries);
         bool Delete(T Entry);
         bool Delete(int Id);
        bool Edit(T Item, int Id);
         IEnumerable<T> GetAll();
         T GetSingle(int Id);
    }
}
