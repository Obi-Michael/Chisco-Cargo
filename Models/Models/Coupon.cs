using System;
using System.Collections.Generic;

namespace Models.Models
{
    public partial class Coupon
    {
        public Guid Id { get; set; }
        public DateTime CreationTime { get; set; }
        public long? CreatorUserId { get; set; }
        public DateTime? LastModificationTime { get; set; }
        public long? LastModifierUserId { get; set; }
        public string? CouponCode { get; set; }
        public decimal CouponValue { get; set; }
        public int CouponType { get; set; }
        public bool Validity { get; set; }
        public int DurationType { get; set; }
        public int Duration { get; set; }
        public string? PhoneNumber { get; set; }
        public DateTime? DateUsed { get; set; }
        public bool IsUsed { get; set; }
        public decimal CouponValueLimit { get; set; }
        public DateTime? EndDate { get; set; }
        public DateTime? StartDate { get; set; }
        public int TerminalId { get; set; }
        public int UserId { get; set; }
        public string? Reference { get; set; }
        public string? CreatorUsername { get; set; }
        public string? LastModifierUsername { get; set; }
    }
}
