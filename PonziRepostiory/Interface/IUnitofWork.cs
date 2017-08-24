﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PonziRepostiory.Interface
{
    public interface IUnitofWork : IDisposable
    {
       
        IPackageRepoitory PackageDetails { get; }
        ICityRepoitory CityDetails { get; }
        IStateRepoitory StateDetails { get; }
        IBankRepoitory BankDetail { get; }
        IUserRepository UserDetails { get; }
        int SaveChanges();
        
    }
}
