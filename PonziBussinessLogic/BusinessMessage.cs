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
        Null=3
    }
    public class BaseMessage
    {
        public ResponseCode ResponseCode { get; set; }
        public string Message { get; set; }
    }
}
