using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace Ponzi.DataContract
{
    [DataContract]
    public class TransactionDetailDTO
    {

        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string Description { get; set; }
        [DataMember]
        public bool IsActive { get; set; }
        [DataMember]
        public int BenefitPercentage { get; set; }
        [DataMember]
        public int MaturityTime { get; set; }
    }
}
