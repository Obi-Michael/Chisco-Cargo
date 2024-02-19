namespace Models.Models
{
    public partial class IdentityProperty
    {
        public int Id { get; set; }
        public string Key { get; set; } = null!;
        public string Value { get; set; } = null!;
        public int IdentityResourceId { get; set; }
    }
}
