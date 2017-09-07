using PonziRepostiory.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PonziRepostiory.Implementation
{

    public class PhoneValidationRepository : Repository<PhoneValidation>, IPhoneValidationRepository
    {
        private NobleDBEntities Context;
        public PhoneValidationRepository(NobleDBEntities Context)
          : base(Context)
        {
            this.Context = Context;
        }
     

        public bool ConfirmPhoneCode(string userId, string code)
        {
            return Context.Verifications.OfType<PhoneValidation>().Where(u => u.User.Username.ToLower().Trim() == userId.ToLower().Trim() &&
               u.PhoneCode == code).Any();
        }

        public PhoneValidation GetRecentPhoneCode(string userId)
        {
            return Context.Verifications.OfType<PhoneValidation>().Where(c => c.User.Username.ToLower() == userId.ToLower()).OrderByDescending(m => m.Id).FirstOrDefault();
         
        }

        public bool PhoneIsValid(string userEmail)
        {
            return Context.Verifications.OfType<PhoneValidation>().Where(u => u.User.EmailAddress == userEmail &&
            u.Status == (int)ValidationStatusEnum.Phone_Code_Generated).Any();
        }
    }
}
