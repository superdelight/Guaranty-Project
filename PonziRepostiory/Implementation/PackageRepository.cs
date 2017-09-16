using PonziRepostiory.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PonziRepostiory.Implementation
{
    public class PackageRepository : Repository<MPackage>, IPackageRepoitory
    {
        private NobleDBEntities Context;
        public PackageRepository(NobleDBEntities Context)
          : base(Context)
        {
            this.Context = Context;
        }

        public bool ConfirmPackage(string packageDescription)
        {
            return Context.MPackages.Any(c => c.Description.ToLower() == packageDescription.ToLower());
        }

        public MPackage GetDefaultPackage()
        {
          
            return Context.MPackages.Where(c=>c.IsActive==true).FirstOrDefault();
        }
    }
}
