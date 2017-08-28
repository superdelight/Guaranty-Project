using PonziRepostiory.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PonziRepostiory.Implementation
{

    public class ConfirmationStatusRepository : Repository<ConfirmationStatus>, IConfirmationStatusRepository
    {
        private NobleDBEntities Context;
        public ConfirmationStatusRepository(NobleDBEntities Context)
          : base(Context)
        {
            this.Context = Context;
        }
        public IEnumerable<ConfirmationStatus> GetAllConfirmationStatus()
        {
            var confirmationStatus = Context.ConfirmationStatus1.ToList();
            if(confirmationStatus==null)
            {
                confirmationStatus = new List<PonziRepostiory.ConfirmationStatus>();
                confirmationStatus.Add(new PonziRepostiory.ConfirmationStatus(){Description = "Pending" });
                confirmationStatus.Add(new PonziRepostiory.ConfirmationStatus() { Description = "Confirmed" });
                confirmationStatus.Add(new PonziRepostiory.ConfirmationStatus() { Description = "Not_Confirmed" });
                confirmationStatus.Add(new PonziRepostiory.ConfirmationStatus() { Description = "Admin_Confirmed" });
                Context.ConfirmationStatus1.AddRange(confirmationStatus);
                Context.SaveChanges();
                confirmationStatus = Context.ConfirmationStatus1.ToList();
            }
            return confirmationStatus;
        }
    }
}
