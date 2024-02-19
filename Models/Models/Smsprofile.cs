namespace Models.Models
{
    public partial class Smsprofile
    {
        public int Id { get; set; }
        public string AppName { get; set; } = null!;
        public double? SmsminQty { get; set; }
        public bool? ConfirmEmail { get; set; }
        public string? SmtpAddress { get; set; }
        public string? Port { get; set; }
        public string? Username { get; set; }
        public string? Password { get; set; }
        public string? Profile { get; set; }
        public string? SmsuserName { get; set; }
        public string? Smspassword { get; set; }
        public string? SmssubUserName { get; set; }
        public bool? IsActive { get; set; }
    }
}
