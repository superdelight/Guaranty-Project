using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PonziRepostiory.Interface
{
    public interface IUnitofWork : IDisposable
    {
        ITransDetailRepoitory TransactionDetail { get; }
        ITransSplitRepoitory TransactionSplitDetail { get; }
        IPackageRepoitory PackageDetails { get; }
        ICityRepoitory CityDetails { get; }
        IStateRepoitory StateDetails { get; }
        IUserStatusRepoitory UserStatusDetail { get; }
        IBankRepoitory BankDetail { get; }
        IUserRepository UserDetails { get; }
        int SaveChanges();
        
    }
}
