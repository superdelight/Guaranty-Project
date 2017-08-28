using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PonziRepostiory.Interface
{
    
    public interface IProxyRepository : IRepository<ProxySetting>
    {
        ProxySetting GetActiveProxySetting();
        bool SendEmail(bool IsHTMLBody, string message, string header, string recepientEmail);
        bool SendEmail(bool IsHTMLBody, byte[] attachment, string docDescription,string message, string header,string destinationEmail);
    }
}
