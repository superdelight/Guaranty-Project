using PonziRepostiory.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PonziRepostiory.Implementation
{
    public class UserPackageRepository : Repository<UserPackage>, IUserPackageRepoitory
    {
        private NobleDBEntities Context;
        public UserPackageRepository(NobleDBEntities Context)
          : base(Context)
        {
            this.Context = Context;
        }

        public bool ConfirmPackageEligibility(int userId, double newAmount)
        {
            return (from c in Context.UserPackages where userId == userId && c.Amount > newAmount select c).Any();
        }

        public IEnumerable<UserPackage> GetAllUserPackages(int statusId)
        {
            return Context.UserPackages.Where(c => c.PackStatus == statusId).ToList();
        }

        public UserPackage GetCurrentUserPackage(int userId, int statusId)
        {
            return Context.UserPackages.Where(c => c.PackStatus == statusId && c.UserId==userId).FirstOrDefault();
        }
    }
}
