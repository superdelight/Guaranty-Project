using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Ponzi.Implementation.Contract
{
    [ServiceContract]
    public interface IPonziAdminService
    {
        [OperationContract]
        bool CreateNewPackage(string Description, int BenefitPercentage, bool IsActive, int maturityDate);
    }
}
