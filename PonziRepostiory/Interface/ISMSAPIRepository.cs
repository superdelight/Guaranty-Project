using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PonziRepostiory.Interface
{
    
    public interface ISMSAPIRepository : IRepository<SMSSetting>
    {
        bool SendSMS(string destinationPhoneNo, string message, string header);
    }
}
