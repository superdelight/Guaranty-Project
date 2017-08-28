using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PonziRepostiory.Interface
{
    
    public interface IPhoneValidationRepository : IRepository<PhoneValidation>
    {
        bool PhoneIsValid(string userEmail);
        bool ConfirmPhoneCode(string userId, string code);
    }
}
