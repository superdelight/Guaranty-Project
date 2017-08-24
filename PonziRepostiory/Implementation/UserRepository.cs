using PonziRepostiory.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PonziRepostiory.Implementation
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        private NobleDBEntities Context;
        public UserRepository(NobleDBEntities Context)
          : base(Context)
        {
            this.Context = Context;
        }
      
        public bool AccountIsActive(string emailAddress)
        {
            return Context.Users.Any(c => c.SortCode.ToLower() == "confirmed");
        }
        public bool ConfirmUserByAccountNo(string AccountNo)
        {
            return Context.Users.Any(d => d.AccountNo == AccountNo);
        }
        public bool ConfirmUserByEmail(string EmailAddress)   
        {
            return Context.Users.Any(d => d.EmailAddress == EmailAddress);
        }
        public bool ConfirmUserByPhoneNumber(string PhoneNumber)
        {
            return Context.Users.Any(d => d.PhoneNo == PhoneNumber);
        }

        public IEnumerable<User> GetAllUsers(int statusId)
        {
            return Context.Users.Where(c => c.NobleStatus == statusId).ToList();
        }

        public User GetUserByAccountNo(string accNo)
        {
            return Context.Users.Single(c => c.AccountNo == accNo);
        }
        public User GetUserByEmail(string emailAddress)
        {
            return Context.Users.Single(c => c.EmailAddress == emailAddress);
        }

        public bool IsRole(string emailAddress, int Status)
        {
            throw new NotImplementedException();
        }
    }
}
