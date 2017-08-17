using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GuarantyWeb.Areas.PonziAdmin.Models
{
    public class TransDetail
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<int> BenefitPercentage { get; set; }
        public Nullable<int> MaturityTime { get; set; }
    }
}