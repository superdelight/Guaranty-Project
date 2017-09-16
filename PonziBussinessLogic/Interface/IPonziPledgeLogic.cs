using PonziRepostiory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PonziBussinessLogic.Interface
{
    public interface IPonziPledgeLogic
    {
        BusinessMessage<string> GetPackageMessage(string username);
        BusinessMessage<bool> CreateUserPackage(UserPackage Package);
        UserPackage GetUserActivePackage(string userID);
    }
}
