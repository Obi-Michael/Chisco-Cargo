namespace Models.Models
{
    public partial class IdentityClaim
    {
        public int Id { get; set; }
        public string Type { get; set; } = null!;
        public int IdentityResourceId { get; set; }
    }
}
