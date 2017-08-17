using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PonziRepostiory.Interface
{
    
    public interface ICityRepoitory : IRepository<City>
    {
        IEnumerable<City> GetAllCities(int StateId);
    }
}
