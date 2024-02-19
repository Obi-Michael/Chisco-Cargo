namespace Models.Models
{
    public partial class AspNetUserToken
    {
        public int UserId { get; set; }
        public string LoginProvider { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string? Value { get; set; }
    }
}
