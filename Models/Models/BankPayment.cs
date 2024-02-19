using System;
using System.Collections.Generic;

namespace Models.Models
{
    public partial class BankPayment
    {
        public int Id { get; set; }
        public DateTime CreationTime { get; set; }
        public long? CreatorUserId { get; set; }
        public DateTime? LastModificationTime { get; set; }
        public long? LastModifierUserId { get; set; }
        public bool IsDeleted { get; set; }
        public long? DeleterUserId { get; set; }
        public DateTime? DeletionTime { get; set; }
        public DateTime PaymentDate { get; set; }
        public int BankId { get; set; }
        public string? TellerNumber { get; set; }
        public string? Depositor { get; set; }
        public double Amount { get; set; }
        public int TerminalId { get; set; }
        public int AccountingStatus { get; set; }
        public string? AuthorisedBy { get; set; }
    }
}
