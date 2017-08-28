using PonziRepostiory.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace PonziRepostiory.Implementation
{
    public class SMSAPIRepository : Repository<SMSSetting>, ISMSAPIRepository
    {
        private NobleDBEntities Context;
        public SMSAPIRepository(NobleDBEntities Context)
          : base(Context)
        {
            this.Context = Context;
        }

        public ProxySetting GetActiveProxySetting()
        {
            return Context.ProxySettings.Where(c => c.IsActive == true).FirstOrDefault();
        }

        public SMSSetting GetActiveSMS()
        {
            return Context.SMSSettings.Where(c => c.IsActive == true).FirstOrDefault();

        }

        public bool SendSMS(string destinationPhoneNo,string message,string header)
        {
            try
            {
                var activeDet = Context.SMSSettings.Where(c => c.IsActive == true).FirstOrDefault();

                string msg = string.Format("{0}?username={1}&password={2}&message={3}&sender={4}&mobiles={5}", activeDet.SMSAPI, activeDet.Username, activeDet.Password, message, header, destinationPhoneNo);
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(msg);
                request.Timeout = 900000;
                var response = (HttpWebResponse)request.GetResponse();
                string m = response.StatusDescription;
                response.Close();
                bool bbb = false;
                if (m.ToLower() == "ok")
                {
                    bbb = true;
                }
                return bbb;
            }
            catch
            {
                return false;
            }
        }
    }
}
