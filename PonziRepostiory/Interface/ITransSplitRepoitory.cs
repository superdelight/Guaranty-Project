using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PonziRepostiory.Interface
{
    
    public interface ITransSplitRepoitory : IRepository<TransactionSplit>
    {
        IEnumerable<TransactionSplit> GetAllTransactionSplit(int transId);
        bool ConfirmTransactionSplit(string description);
    }
}
