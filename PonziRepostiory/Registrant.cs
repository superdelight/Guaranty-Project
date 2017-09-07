//------------------------------------------------------------------------------
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
    using System.Collections.Generic;
    
    public partial class Registrant
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Registrant()
        {
            this.User1 = new HashSet<Registrant>();
            this.UserPackages = new HashSet<UserPackage>();
            this.Verifications = new HashSet<Verification>();
        }
    
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Surname { get; set; }
        public string Othername { get; set; }
        public string Gender { get; set; }
        public string PhoneNo { get; set; }
        public string EmailAddress { get; set; }
        public Nullable<System.DateTime> DateCreated { get; set; }
        public Nullable<int> BankId { get; set; }
        public string AccountNo { get; set; }
        public string AccountName { get; set; }
        public string SortCode { get; set; }
        public Nullable<int> CityId { get; set; }
        public Nullable<int> NobleStatus { get; set; }
        public Nullable<System.DateTime> LastLoginDate { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<int> ReferralId { get; set; }
    
        public virtual Bank Bank { get; set; }
        public virtual City City { get; set; }
        public virtual RoleStatus RoleStatu { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Registrant> User1 { get; set; }
        public virtual Registrant User2 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<UserPackage> UserPackages { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Verification> Verifications { get; set; }
    }
}