using System;
using System.Collections.Generic;

namespace Models.Models
{
    public partial class CashRemittant
    {
        public Guid Id { get; set; }
        public DateTime CreationTime { get; set; }
        public long? CreatorUserId { get; set; }
        public DateTime? LastModificationTime { get; set; }
        public long? LastModifierUserId { get; set; }
        public string? Ticketer { get; set; }
        public string? Accountant { get; set; }
        public double Amount { get; set; }
        public int TerminalId { get; set; }
        public int AccountingStatus { get; set; }
        public string? AuthorisedBy { get; set; }
    }
}
