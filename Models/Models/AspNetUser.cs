using System;
using System.Collections.Generic;

namespace Models.Models
{
    public partial class AspNetUser
    {
        public AspNetUser()
        {
            Employees = new HashSet<Employee>();
        }

        public int Id { get; set; }
        public string? UserName { get; set; }
        public string? NormalizedUserName { get; set; }
        public string? Email { get; set; }
        public string? NormalizedEmail { get; set; }
        public bool EmailConfirmed { get; set; }
        public string? PasswordHash { get; set; }
        public string? SecurityStamp { get; set; }
        public string? ConcurrencyStamp { get; set; }
        public string? PhoneNumber { get; set; }
        public bool PhoneNumberConfirmed { get; set; }
        public bool TwoFactorEnabled { get; set; }
        public DateTimeOffset? LockoutEnd { get; set; }
        public bool LockoutEnabled { get; set; }
        public int AccessFailedCount { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public bool IsFirstTimeLogin { get; set; }
        public string? OptionalPhoneNumber { get; set; }
        public int UserType { get; set; }
        public string? Image { get; set; }
        public DateTime CreationTime { get; set; }
        public DateTime? DeletionTime { get; set; }
        public bool IsDeleted { get; set; }
        public string? RefreshToken { get; set; }
        public string? DeviceToken { get; set; }
        public int Gender { get; set; }
        public int LoginDeviceType { get; set; }
        public string? NextOfKinName { get; set; }
        public string? NextOfKinPhone { get; set; }
        public DateTime? LastModificationTime { get; set; }
        public string? ReferralCode { get; set; }
        public string? AccountConfirmationCode { get; set; }
        public string? Title { get; set; }
        public long? WalletId { get; set; }
        public string? Address { get; set; }
        public string? DateOfBirth { get; set; }
        public string? MiddleName { get; set; }
        public string? Photo { get; set; }
        public string? CustomerCode { get; set; }
        public string? Referrer { get; set; }
        public string? Otp { get; set; }
        public int? CompanyId { get; set; }
        public int? LocationId { get; set; }
        public int? WalletId1 { get; set; }

        public virtual ICollection<Employee> Employees { get; set; }
    }
}
