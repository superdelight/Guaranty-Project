using PonziRepostiory.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PonziRepostiory.Implementation
{
    public class TransDetRepository:Repository<TransactionDetail>, ITransDetailRepoitory
    {
        private NobleDBEntities Context;
        public TransDetRepository(NobleDBEntities Context)
          : base(Context)
        {
            this.Context = Context;
        }

        public bool ConfirmTransaction(string transDes)
        {
            return Context.TransactionDetails.Any(c => c.Description.ToLower() == transDes.ToLower());
        }
    }
}
