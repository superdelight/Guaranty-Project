using PonziRepostiory.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PonziRepostiory.Implementation
{

    public class PackageStatusRepository : Repository<PackageStatus>, IPackageStatusRepoitory
    {
        private NobleDBEntities Context;
        public PackageStatusRepository(NobleDBEntities Context)
          : base(Context)
        {
            this.Context = Context;
        }

        public IEnumerable<PackageStatus> GetAllPackageStatus()
        {
            
            var packageStatus = Context.PackageStatus1.ToList();
            if (packageStatus == null)
            {
                packageStatus = new List<PackageStatus>();
                packageStatus.Add(new PonziRepostiory.PackageStatus() { Description = "Pending" });
                packageStatus.Add(new PonziRepostiory.PackageStatus() { Description = "Active" });
                packageStatus.Add(new PonziRepostiory.PackageStatus() { Description = "Disabled" });
                packageStatus.Add(new PonziRepostiory.PackageStatus() { Description = "Closed" });
                Context.PackageStatus1.AddRange(packageStatus);
                Context.SaveChanges();
                packageStatus = Context.PackageStatus1.ToList();
            }
            return packageStatus;
        }
    }
}
