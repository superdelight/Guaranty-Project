using PonziRepostiory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ponzi.DataContract.MappingDTO
{
   
    public static class MappingsExtension
    {
        public static bool Contains(this string source, string toCheck, StringComparison comp)
        {
            return source != null && toCheck != null && source.IndexOf(toCheck, comp) >= 0;
        }
        public static TransactionDetailDTO ToDTO(this TransactionDetail source)
        {
            return new TransactionDetailDTO()
            {
                Description = source.Description,
                IsActive = (bool)source.IsActive,
                MaturityTime = (int)source.MaturityTime,
                BenefitPercentage=(int)source.BenefitPercentage
            };
        }
        public static List<TransactionDetailDTO> ToDTO(this List<TransactionDetail> source)
        {
            List<TransactionDetailDTO> groups = new List<TransactionDetailDTO>();
            source.ForEach(o => groups.Add(o.ToDTO()));
            return groups;
        }
    }
}
