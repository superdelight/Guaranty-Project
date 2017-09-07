using PonziRepostiory.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PonziRepostiory.Implementation
{

    public class RoleStatusRepository : Repository<RoleStatus>, IRoleStatusRepoitory
    {
        private NobleDBEntities Context;
        public RoleStatusRepository(NobleDBEntities Context)
          : base(Context)
        {
            this.Context = Context;
        }

        public IEnumerable<RoleStatus> GetAllRoleStatus()
        {

            var roleStatus = Context.RoleStatus1.ToList();
            if (roleStatus.Count == 0)
            {
                roleStatus = new List<RoleStatus>();
                roleStatus.Add(new PonziRepostiory.RoleStatus() { Description = "Outbound_User" });
                roleStatus.Add(new PonziRepostiory.RoleStatus() { Description = "General_Admin" });
                roleStatus.Add(new PonziRepostiory.RoleStatus() { Description = "Super_Admin" });
                roleStatus.Add(new PonziRepostiory.RoleStatus() { Description = "Promoted_Privilege" });

                Context.RoleStatus1.AddRange(roleStatus);
                Context.SaveChanges();
                roleStatus = Context.RoleStatus1.ToList();
            }
            return roleStatus;

        }
    }
}
