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
            return Context.Verifications.OfType<PhoneValidation>().Where(u => u.User.EmailAddress == userId &&
               u.PhoneCode == code).Any();
        }


        public bool PhoneIsValid(string userEmail)
        {
            return Context.Verifications.OfType<PhoneValidation>().Where(u => u.User.EmailAddress == userEmail &&
            u.Status == (int)ValidationStatusEnum.Phone_Generated).Any();
        }
    }
}
