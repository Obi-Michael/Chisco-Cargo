using System;
using System.Collections.Generic;

namespace Models.Models
{
    public partial class Agent
    {
        public int Id { get; set; }
        public DateTime CreationTime { get; set; }
        public long? CreatorUserId { get; set; }
        public DateTime? LastModificationTime { get; set; }
        public long? LastModifierUserId { get; set; }
        public bool IsDeleted { get; set; }
        public long? DeleterUserId { get; set; }
        public DateTime? DeletionTime { get; set; }
        public int UserId { get; set; }
        public int AgentLocationId { get; set; }
        public int RoleId { get; set; }
        public int AgentType { get; set; }
        public string? CompanyAddress { get; set; }
        public string? CompanyEmail { get; set; }
        public string? CompanyName { get; set; }
        public string? CompanyPhoneNumber { get; set; }
        public string? NatureOfBusiness { get; set; }
        public decimal? WalletBalance { get; set; }
        public int WalletId { get; set; }
    }
}
