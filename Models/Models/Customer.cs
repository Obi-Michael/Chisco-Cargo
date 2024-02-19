namespace Models.Models
{
    public partial class Customer
    {
        public int Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public int Gender { get; set; }
        public string? Email { get; set; }
        public string? Address { get; set; }
        public string PhoneNumber { get; set; } = null!;
        public string? Password { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime? DateModified { get; set; }
        public bool IsDeleted { get; set; }
        public string? CodeFromReferrer { get; set; }
        public string? ReferralCode { get; set; }
        public string? UserId { get; set; }
        public DateTime? DateofBirth { get; set; }
    }
}
