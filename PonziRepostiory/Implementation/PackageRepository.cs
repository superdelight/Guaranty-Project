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

        public bool ConfirmPackage(string packageDescription)
        {
            return Context.Packages.Any(c => c.Description.ToLower() == packageDescription.ToLower());
        }

        public Package GetDefaultPackage()
        {
            return Context.Packages.FirstOrDefault();
        }
    }
}
