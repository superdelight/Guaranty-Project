using PonziRepostiory.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PonziRepostiory.Implementation
{
    public class TransSplitRepository : Repository<TransactionSplit>, ITransSplitRepoitory
    {
        private NobleDBEntities Context;
        public TransSplitRepository(NobleDBEntities Context)
          : base(Context)
        {
            this.Context = Context;
        }

        public bool ConfirmTransactionSplit(string description)
        {
            return Context.TransactionSplits.Any(c => c.SplitDescription.ToLower() == description.ToLower());
        }

        public IEnumerable<TransactionSplit> GetAllTransactionSplit(int transId)
        {
            return (from c in Context.TransactionSplits
                    where c.TransId == transId select c).ToList();
        }
    }
}
