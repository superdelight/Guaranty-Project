using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PonziRepostiory.Interface
{
    
    public interface IUserPackageRepoitory : IRepository<UserPackage>
    {
        IEnumerable<UserPackage> GetAllUserPackages(int statusId);
        UserPackage GetCurrentUserPackage(int userId, int statusId);
        UserPackage GetUserLastPackage(int userId);
        UserPackage GetUserLastPackage(string userId);
        bool ConfirmPackageEligibility(int userId, double newAmount);
    }
}
