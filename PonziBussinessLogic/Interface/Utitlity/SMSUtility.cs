using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace PonziBussinessLogic.Utitlity
{
    public class SMSUtility
    {

        public string PhoneNo { get; set; }
        public string Message { get; set; }
        public string APIAddress { get; set; }
        public string EmailSource { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string MessageHeader { get; set; }

        public bool SendSMS()
        {
            try
            {
                // http://portal.bulksmsnigeria.net/api/?username=user&password=pass&message=test&sender=welcome&mobiles=2348030000000,2348020000000
                //  http://portal.bulksmsnigeria.net/api/?username=user&password=pass&message=391000&sender=2348030000000&mobiles=2348020000000&type=call
                //http://portal.bulksmsnigeria.net/api/?username=motionline16@gmail.com&password=noblecommunity&message=How&sender=2348030000000&mobiles=2348060168634&type=call
               // string msg = string.Format("{0}?username={1}&password={2}&message={3}&sender={4}&mobiles={5}&type=call", APIAddress, Username, Password, Message, MessageHeader, PhoneNo);
                string msg = string.Format("{0}?username={1}&password={2}&message={3}&sender={4}&mobiles={5}", APIAddress, Username, Password, Message, MessageHeader, PhoneNo);
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

