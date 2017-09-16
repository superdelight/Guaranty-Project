using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PonziBussinessLogic
{
    public class BusinessMessage<T>:BaseMessage
    {
       public T Result { get; set; }
    }
    public enum ResponseCode
    {
        OK=1,
        NotOK=2,
        Null=3,
        Active=4,
        Matched_For_PH=5,
        Matched_For_GH=6,
        Closed=7,
        Error=8

    }
    public class BaseMessage
    {
        public ResponseCode ResponseCode { get; set; }
        public string Message { get; set; }
    }
}
