using PonziRepostiory.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PonziRepostiory.Implementation
{

    public class EmailValidationRepository : Repository<EmailValidation>, IEmailValidationRepository
    {
        private NobleDBEntities Context;
        public EmailValidationRepository(NobleDBEntities Context)
          : base(Context)
        {
            this.Context = Context;
        }
        public bool ConfirmEmailCode(string userId, string code)
        {
            return Context.Verifications.OfType<EmailValidation>().Where(u => u.User.EmailAddress == userId &&
            u.EmailCode == code).Any();
        }
        public bool EmailIsValid(string userEmail)
        {

            return Context.Verifications.OfType<EmailValidation>().Where(u => u.User.EmailAddress == userEmail &&
            u.Status == (int)ValidationStatusEnum.Email_Verified).Any();
        } 
    }
}
