using PonziRepostiory.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PonziRepostiory.Implementation
{

    public class TransactionStatusRepository : Repository<TransactionStatus>, ITransactionStatusRepoitory
    {
        private NobleDBEntities Context;
        public TransactionStatusRepository(NobleDBEntities Context)
          : base(Context)
        {
            this.Context = Context;
        }

        public IEnumerable<RoleStatus> GetAllRoleStatus()
        {

            var roleStatus = Context.RoleStatus1.ToList();
            if (roleStatus == null)
            {
                roleStatus = new List<RoleStatus>();
                roleStatus.Add(new PonziRepostiory.RoleStatus() { Description = "Outbound_User" });
                roleStatus.Add(new PonziRepostiory.RoleStatus() { Description = "General_Admin" });
                roleStatus.Add(new PonziRepostiory.RoleStatus() { Description = "Super_Admin" });
                roleStatus.Add(new PonziRepostiory.RoleStatus() { Description = "Promoted_Privilege" });

                Context.SaveChanges();
                roleStatus = Context.RoleStatus1.ToList();
            }
            return roleStatus;

        }

        public IEnumerable<TransactionStatus> GetAllTransactionStatus()
        {
           
            var transactionStatus = Context.TransactionStatus1.ToList();
            if (transactionStatus == null)
            {
                transactionStatus = new List<TransactionStatus>();
                transactionStatus.Add(new PonziRepostiory.TransactionStatus() { Description = "PH_Pending" });
                transactionStatus.Add(new PonziRepostiory.TransactionStatus() { Description = "PH_Confirmed" });
                transactionStatus.Add(new PonziRepostiory.TransactionStatus() { Description = "PH_Provisioned" });
                transactionStatus.Add(new PonziRepostiory.TransactionStatus() { Description = "PH_Closed" });
                transactionStatus.Add(new PonziRepostiory.TransactionStatus() { Description = "GH_Process_Begins" });
                transactionStatus.Add(new PonziRepostiory.TransactionStatus() { Description = "GH_Process_Completed" });
                transactionStatus.Add(new PonziRepostiory.TransactionStatus() { Description = "GH_Process_Closed" });
                transactionStatus.Add(new PonziRepostiory.TransactionStatus() { Description = "GH_Floating" });
                transactionStatus.Add(new PonziRepostiory.TransactionStatus() { Description = "Disabled" });
                Context.SaveChanges();

                transactionStatus = Context.TransactionStatus1.ToList();
            }
            return transactionStatus;
        }
    }
}
