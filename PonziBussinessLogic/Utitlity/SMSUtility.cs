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
                string msg = string.Format("{0}?username={1}&password={2}&sender={3}&recipient={4}&message={5}&", APIAddress, Username, Password, MessageHeader, PhoneNo, Message);
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

