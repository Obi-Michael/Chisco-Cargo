using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Models
{
    public class MerchantShipment
    {
        public int Id { get; set; }

        public int MerchantID { get; set; }

        public decimal TotalGrandTotal { get; set; }

        public decimal TotalVat { get; set; }

        public decimal TotalInsured { get; set; }

        public DateTime DateCreated { get; set; }

        public bool IsDeleted { get; set; }

        public MerchantSignup MerchantSignup { get; set; }
    }
}
