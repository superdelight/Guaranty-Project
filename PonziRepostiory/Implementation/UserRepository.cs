using PonziRepostiory.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PonziRepostiory.Implementation
{
    public class UserRepository : Repository<Registrant>, IUserRepository
    {
        private NobleDBEntities Context;
        public UserRepository(NobleDBEntities Context)
          : base(Context)
        {
            this.Context = Context;
        }
      
        public bool AccountIsActive(string emailAddress)
        {
            return Context.Registrants.Any(c => c.SortCode.ToLower() == "confirmed");
        }
        public bool ConfirmUserByAccountNo(string AccountNo)
        {
            return Context.Registrants.Any(d => d.AccountNo == AccountNo);
        }
        public bool ConfirmUserByEmail(string EmailAddress)   
        {
            return Context.Registrants.Any(d => d.EmailAddress == EmailAddress);
        }
        public bool ConfirmUserByPhoneNumber(string PhoneNumber)
        {
            return Context.Registrants.Any(d => d.PhoneNo == PhoneNumber);
        }

        public IEnumerable<Registrant> GetAllUsers(int statusId)
        {
            return Context.Registrants.Where(c => c.NobleStatus == statusId).ToList();
        }

        public Registrant GetUser(string loginId)
        {
            return Context.Registrants.Where(c => c.Username.Trim().ToLower() == loginId.Trim().ToLower()).FirstOrDefault();
        }

        public Registrant GetUserByAccountNo(string accNo)
        {
            return Context.Registrants.Single(c => c.AccountNo == accNo);
        }
        public Registrant GetUserByEmail(string emailAddress)
        {
            return Context.Registrants.Single(c => c.EmailAddress == emailAddress);
        }

        public bool IsRole(string emailAddress, int Status)
        {
            throw new NotImplementedException();
        }
    }
}
