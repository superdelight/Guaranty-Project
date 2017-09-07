using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PonziRepostiory.Interface
{
    
    public interface IUserRepository : IRepository<Registrant>
    {
        bool ConfirmUserByAccountNo(string AccountNo);
        bool AccountIsActive(string emailAddress);
        bool ConfirmUserByPhoneNumber(string PhoneNumber);
        bool ConfirmUserByEmail(string EmailAddress);
        Registrant GetUserByEmail(string emailAddress);
        Registrant GetUserByAccountNo(string accNo);
        IEnumerable<Registrant> GetAllUsers(int statusId);
        bool IsRole(string emailAddress, int Status);
    }
}
