using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Models
{
    public class ShipmentCollection
    {
        public string WayBillNumber { get; set; }

        public string ReceiverName { get; set; }

        public string PhoneNumber { get; set; }

        public string Email { get; set; }

        public string Address { get; set; }

        public string State { get; set; }

        public string MeansOfID { get; set; }

        public string Addcomment { get; set; }

        public string ReleasedBy { get; set; }

        public DateTime? DateCreated { get; set; }

        public DateTime? DateModified { get; set; }

        public bool IsDeleted { get; set; }
    }
}
