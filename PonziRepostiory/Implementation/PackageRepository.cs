using PonziRepostiory.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PonziRepostiory.Implementation
{
    public class PackageRepository : Repository<Package>, IPackageRepoitory
    {
        private NobleDBEntities Context;
        public PackageRepository(NobleDBEntities Context)
          : base(Context)
        {

        }

        public IEnumerable<Package> GetAllPackages(int transId)
        {
            return (from c in Context.Packages where c.TransId == transId select c).ToList();
        }
    }
}
