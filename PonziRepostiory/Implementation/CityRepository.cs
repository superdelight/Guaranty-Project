using PonziRepostiory.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PonziRepostiory.Implementation
{
    public class CityRepository : Repository<City>, ICityRepoitory
    {
        private NobleDBEntities Context;
        public CityRepository(NobleDBEntities Context)
          : base(Context)
        {
            this.Context = Context;
        }

        public IEnumerable<City> GetAllCities(int StateId)
        {
            return Context.Cities.Where(c => c.StateId == StateId);
        }
    }
}
