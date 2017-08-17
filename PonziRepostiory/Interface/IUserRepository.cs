using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PonziRepostiory.Interface
{
    
    public interface IUserRepository : IRepository<User>
    {
        bool ConfirmUserByAccountNo(string AccountNo);
        bool AccountIsActive(string emailAddress);
        bool ConfirmUserByPhoneNumber(string PhoneNumber);
        bool ConfirmUserByEmail(string EmailAddress);
        User GetUserByEmail(string emailAddress);
        User GetUserByAccountNo(string accNo);
        IEnumerable<User> GetAllUsers(int statusId);
    }
}
