using PonziRepostiory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PonziBussinessLogic.Interface
{
    public interface IPonziAdminLogic
    {
        BusinessMessage<bool> CreateNewPackage(MPackage package);
        BusinessMessage<MPackage> GetSinglePackage(int packageId);
        BusinessMessage<MPackage> GetDefaultPackage();

        BusinessMessage<Bank> GetSingleBank(int Id);
        BusinessMessage<List<Bank>> GetAllBanks();
        BusinessMessage<City> GetSingleCity(int Id);
        BusinessMessage<List<City>> GetAllCities(int stateId);
        BusinessMessage<List<State>> GetAllStates();

        BusinessMessage<bool> CreateNewEmailVerification(EmailValidation EmailVerification);
        BusinessMessage<EmailValidation> GetRegistrantEmailVerification(string email);
        bool VerifyEmail(string email);
        BusinessMessage<bool> VerifyEmail(string email,string code);

        BusinessMessage<bool> CreateNewPhoneVerificaton(PhoneValidation phoneValidation);
        BusinessMessage<PhoneValidation> GetRegistrantPhoneVerification(string email);
        BusinessMessage<bool> VerifyPhone(string email);
        BusinessMessage<bool> VerifyPhone(string email,string code);
        bool ActivateUserEmail(string userId,string code);
        bool ActivateUserPhoneNumber(string userId, string code);
        BusinessMessage<bool> CreateNewRegistrant(Registrant Registrant);
        BusinessMessage<bool> EditRegistrant(Registrant Registrant);
        BusinessMessage<Registrant> GetRegistrantFromAccountNo(string AccNo);
        BusinessMessage<Registrant> GetRegistrantFromEmailAddress(string EmailAddress);
        BusinessMessage<Registrant> GetRegistrant(string loginId);
        BusinessMessage<List<Registrant>> GetAllRegistrants();
        BusinessMessage<List<Registrant>> GetAllRegistrants(int statusId);

        BusinessMessage<string> GetPhoneCode();
        BusinessMessage<string> GetEmailCode();



    }
}
