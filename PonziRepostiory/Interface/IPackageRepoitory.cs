using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PonziRepostiory.Interface
{
    
    public interface IPackageRepoitory : IRepository<Package>
    {
        IEnumerable<Package> GetAllPackages(int transId);
    }
}
