﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace PonziRepostiory
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class NobleDBEntities : DbContext
    {
        public NobleDBEntities()
            : base("name=NobleDBEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Bank> Banks { get; set; }
        public virtual DbSet<City> Cities { get; set; }
        public virtual DbSet<ConfirmationStatus> ConfirmationStatus1 { get; set; }
        public virtual DbSet<Matching> Matchings { get; set; }
        public virtual DbSet<MatchingStatus> MatchingStatus1 { get; set; }
        public virtual DbSet<PackageStatus> PackageStatus1 { get; set; }
        public virtual DbSet<RoleStatus> RoleStatus1 { get; set; }
        public virtual DbSet<State> States { get; set; }
        public virtual DbSet<TransactionDet> TransactionDets { get; set; }
        public virtual DbSet<TransactionStatus> TransactionStatus1 { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<UserPackage> UserPackages { get; set; }
        public virtual DbSet<ValidationStatus> ValidationStatus1 { get; set; }
        public virtual DbSet<Verification> Verifications { get; set; }
        public virtual DbSet<Package> Packages { get; set; }
        public virtual DbSet<ProxySetting> ProxySettings { get; set; }
        public virtual DbSet<SMSSetting> SMSSettings { get; set; }
    }
}
