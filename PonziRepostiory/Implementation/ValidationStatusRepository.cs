using PonziRepostiory.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PonziRepostiory.Implementation
{

    public class ValidationStatusRepository : Repository<ValidationStatus>, IValidationStatusRepository
    {
        private NobleDBEntities Context;
        public ValidationStatusRepository(NobleDBEntities Context)
          : base(Context)
        {
            this.Context = Context;
        }
   
        public IEnumerable<ValidationStatus> GetValidationAllStatus()
        {
           
            var validationStatus = Context.ValidationStatus1.ToList();
            if (validationStatus.Count <= 0)
            {
                validationStatus = new List<ValidationStatus>();
                validationStatus.Add(new PonziRepostiory.ValidationStatus() { Description = "Email_Code_Generated" });
                validationStatus.Add(new PonziRepostiory.ValidationStatus() { Description = "Phone_Code_Generated" });
                validationStatus.Add(new PonziRepostiory.ValidationStatus() { Description = "Email_Verified" });
                validationStatus.Add(new PonziRepostiory.ValidationStatus() { Description = "Phone_Generated" });
                Context.ValidationStatus1.AddRange(validationStatus);
                Context.SaveChanges();
                validationStatus = Context.ValidationStatus1.ToList();
            }
            return validationStatus;
        }
    }
}
