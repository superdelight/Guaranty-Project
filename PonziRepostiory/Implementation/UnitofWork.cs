using PonziRepostiory.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PonziRepostiory.Implementation
{
    public class UnitofWork : IUnitofWork
    {
        private NobleDBEntities _Context;
        public UnitofWork()
        {
            _Context = new NobleDBEntities();
            // this.Context = Context;
         
            PackageDetails = new PackageRepository(_Context);
            BankDetail = new BankRepository(_Context);
            CityDetails = new CityRepository(_Context);
            StateDetails = new StateRepoitory(_Context);
       
         
        }

        public IBankRepoitory BankDetail { get; private set; }
        public ICityRepoitory CityDetails { get; private set; }
        public IPackageRepoitory PackageDetails { get; private set; }

        public IStateRepoitory StateDetails { get; private set; }

        public IUserRepository UserDetails
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public void Dispose()
        {
            
            _Context.Dispose();
        }

        public int SaveChanges()
        {
            try
            {
                return _Context.SaveChanges();
            }
            catch 
            {
                return -1;
            }
        }
    }
}
