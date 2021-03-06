﻿using PonziRepostiory.Interface;
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
            EmailValidationContext = new EmailValidationRepository(_Context);
            PhoneValidationContext = new PhoneValidationRepository(_Context);
            EmailProxyContext = new ProxySettingRepository(_Context);
            SMSProxySetting = new SMSAPIRepository(_Context);
            UserDetails = new UserRepository(_Context);
            UserRoleContext = new RoleStatusRepository(_Context);
            ValidationStatusContext = new ValidationStatusRepository(_Context);
            UserPackageContext = new UserPackageRepository(_Context);
            PackageDetails = new PackageRepository(_Context);
         
        }

        public IBankRepoitory BankDetail { get; private set; }
        public ICityRepoitory CityDetails { get; private set; }

        public IProxyRepository EmailProxyContext { get; private set; }
       
        public IEmailValidationRepository EmailValidationContext { get; private set; }

        public IPackageRepoitory PackageDetails { get; private set; }

        public IPhoneValidationRepository PhoneValidationContext { get; private set; }

        public ISMSAPIRepository SMSProxySetting { get; private set; }

        public IStateRepoitory StateDetails { get; private set; }

        public IUserRepository UserDetails { get; private set; }

        public IUserPackageRepoitory UserPackageContext { get; private set; }
        public IRoleStatusRepoitory UserRoleContext { get; private set; }

        public IValidationStatusRepository ValidationStatusContext { get; private set; }


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
            catch (Exception ex)
            {
                return -1;
            }
        }
    }
}
