using System;
using System.Collections.Generic;

namespace Models.Models
{
    public partial class Complaint
    {
        public int Id { get; set; }
        public string? FullName { get; set; }
        public string? Email { get; set; }
        public int ComplaintType { get; set; }
        public int PriorityLevel { get; set; }
        public string? BookingReference { get; set; }
        public string? Message { get; set; }
        public DateTime CreationTime { get; set; }
        public long? CreatorUserId { get; set; }
        public long? DeleterUserId { get; set; }
        public DateTime? DeletionTime { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? LastModificationTime { get; set; }
        public long? LastModifierUserId { get; set; }
        public DateTime TransDate { get; set; }
        public string? RepliedMessage { get; set; }
        public bool Responded { get; set; }
    }
}
