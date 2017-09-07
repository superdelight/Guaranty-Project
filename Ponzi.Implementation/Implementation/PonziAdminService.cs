using Ponzi.DataContract;
using PonziBussinessLogic;
using PonziBussinessLogic.Interface;
using PonziRepostiory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Ponzi.Implementation.Implementation
{
    [ServiceContract]
    public class PonziAdminService
    {
        IPonziAdminLogic adminLogic;
        public PonziAdminService(IPonziAdminLogic adminLogic)
        {
            this.adminLogic = adminLogic;
        }
        //[OperationContract]
        //BusinessMessage<bool> CreateNewPackage(string Description, int BenefitPercentage, bool IsActive, int maturityDate)
        // {
        //     //TransactionDetail trans = new TransactionDetail { Description = Description, BenefitPercentage = BenefitPercentage, IsActive = IsActive, MaturityTime = maturityDate };
        //     //return adminLogic.CreateNewTransactionDetail(trans);

        // }
        //}
    }

}
