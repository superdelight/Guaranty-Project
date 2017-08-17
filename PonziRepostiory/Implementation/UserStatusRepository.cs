using PonziRepostiory.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PonziRepostiory.Implementation
{
    public class UserStatusRepository : Repository<UserStatu>, IUserStatusRepoitory
    {
        private NobleDBEntities Context;
        public UserStatusRepository(NobleDBEntities Context)
          : base(Context)
        {
            this.Context = Context;
        }

        public UserStatu GetUserCurrentStatus(int userID)
        {
            return (from c in Context.UserStatus from d in c.Users where d.Id == userID select c).FirstOrDefault();
        }
    }
}
