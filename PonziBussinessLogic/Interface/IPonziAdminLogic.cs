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
        BusinessMessage<bool> CreateNewTransactionDetail(TransactionDetail Trans);
        BusinessMessage<TransactionDetail> GetSingleTransactionDetail(int TransId);
        BusinessMessage<bool> EditTransactionDetail(TransactionDetail Trans);
        BusinessMessage<List<TransactionDetail>> GetAllTransactions();

        BusinessMessage<bool> CreateNewTransactionSplit(TransactionSplit Trans);
        BusinessMessage<int> GetTotalSplitMaturityPeriond(int TransId);
        BusinessMessage<TransactionSplit> GetSingleTransactionSplit(int TransId);
        BusinessMessage<List<TransactionSplit>> GetAllTransactionSplit(int transId);
        BusinessMessage<List<TransactionSplit>> GetAllTransactionSplit();


        BusinessMessage<bool> CreateNewPackage(Package Package);
        BusinessMessage<Package> GetSinglePackage(int Id);
        BusinessMessage<List<Package>> GetAllPackages(int transId);
        BusinessMessage<List<Package>> GetAllPackages();


        BusinessMessage<Bank> GetSingleBank(int Id);
        BusinessMessage<List<Bank>> GetAllBanks();
        BusinessMessage<List<UserStatu>> GetAllUserStatus();


        BusinessMessage<City> GetSingleCity(int Id);
        BusinessMessage<List<City>> GetAllCities(int stateId);

        BusinessMessage<List<State>> GetAllStates();


        BusinessMessage<bool> CreateNewUser(User User);
        BusinessMessage<bool> EditUser(User User);
        BusinessMessage<User> GetUserFromAccountNo(string AccNo);
        BusinessMessage<User> GetUserFromEmailAddress(string EmailAddress);
        BusinessMessage<List<User>> GetAllUsers();
        BusinessMessage<List<User>> GetAllUsers(int statusId);
      

    }
}
