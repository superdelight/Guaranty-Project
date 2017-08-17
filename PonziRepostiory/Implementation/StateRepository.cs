using PonziRepostiory.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PonziRepostiory.Implementation
{
    public class StateRepoitory : Repository<State>, IStateRepoitory
    {
        private NobleDBEntities Context;
        public StateRepoitory(NobleDBEntities Context)
          : base(Context)
        {
            this.Context = Context;
        }
    }
}
