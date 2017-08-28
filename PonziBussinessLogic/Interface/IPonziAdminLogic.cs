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
        BusinessMessage<bool> CreateNewPackage(Package package);
        BusinessMessage<Package> GetSinglePackage(int packageId);
        BusinessMessage<Package> GetDefaultPackage();

        BusinessMessage<Bank> GetSingleBank(int Id);
        BusinessMessage<List<Bank>> GetAllBanks();
        BusinessMessage<City> GetSingleCity(int Id);
        BusinessMessage<List<City>> GetAllCities(int stateId);
        BusinessMessage<List<State>> GetAllStates();

        BusinessMessage<bool> CreateNewEmailVerification(EmailValidation EmailVerification);
        BusinessMessage<EmailValidation> GetUserEmailVerification(string email);
        BusinessMessage<bool> VerifyEmail(string email);
        BusinessMessage<bool> VerifyEmail(string email,string code);

        BusinessMessage<PhoneValidation> CreateNewPhoneVerificaton(PhoneValidation phoneValidation);
        BusinessMessage<PhoneValidation> GetUserPhoneVerification(string email);
        BusinessMessage<bool> VerifyPhone(string email);
        BusinessMessage<bool> VerifyPhone(string email,string code);

        BusinessMessage<bool> CreateNewUser(User User);
        BusinessMessage<bool> EditUser(User User);
        BusinessMessage<User> GetUserFromAccountNo(string AccNo);
        BusinessMessage<User> GetUserFromEmailAddress(string EmailAddress);
        BusinessMessage<List<User>> GetAllUsers();
        BusinessMessage<List<User>> GetAllUsers(int statusId);

        BusinessMessage<string> GetPhoneCode();
        BusinessMessage<string> GetEmailCode();

        


    }
}
