using PonziRepostiory.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PonziRepostiory.Implementation
{
    public class BankRepository : Repository<Bank>, IBankRepoitory
    {
        private NobleDBEntities Context;
        public BankRepository(NobleDBEntities Context)
          : base(Context)
        {
            this.Context = Context;
        }
    }
}
