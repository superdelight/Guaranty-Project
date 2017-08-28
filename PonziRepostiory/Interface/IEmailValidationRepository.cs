using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PonziRepostiory.Interface
{
    
    public interface IEmailValidationRepository : IRepository<EmailValidation>
    {
        bool EmailIsValid(string userEmail);
        bool ConfirmEmailCode(string userId, string code);
    }
}
