namespace Models.Models
{
    public partial class Employee
    {
        public int Id { get; set; }
        public DateTime CreationTime { get; set; }
        public long? CreatorUserId { get; set; }
        public DateTime? LastModificationTime { get; set; }
        public long? LastModifierUserId { get; set; }
        public bool IsDeleted { get; set; }
        public long? DeleterUserId { get; set; }
        public DateTime? DeletionTime { get; set; }
        public string? EmployeeCode { get; set; }
        public DateTime? DateOfEmployment { get; set; }
        public int? DepartmentId { get; set; }
        public int? TerminalId { get; set; }
        public string UserId { get; set; }
        public DateTime? OtplastUsedDate { get; set; }
        public string? Otp { get; set; }
        public bool OtpIsUsed { get; set; }
        public int? OtpNoOfTimeUsed { get; set; }
        public string? TicketRemovalOtp { get; set; }
        public bool TicketRemovalOtpIsUsed { get; set; }
        public string? CreatorUsername { get; set; }
        public string? LastModifierUsername { get; set; }
        public decimal? WalletBalance { get; set; }
        public int? WalletId { get; set; }
        public int LocationId { get; set; }

        public virtual AspNetUser User { get; set; } = null!;
    }
}
