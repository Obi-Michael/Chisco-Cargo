using System;
using System.Collections.Generic;

namespace Models.Models
{
    public partial class CouponManagement
    {
        public int Id { get; set; }
        public string? CouponUserId { get; set; }
        public string CouponCode { get; set; } = null!;
        public DateTime UsedDate { get; set; }
        public int? PlatformType { get; set; }
        public string? Waybill { get; set; }
        public bool? IsDeleted { get; set; }
    }
}
