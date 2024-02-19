namespace Models.Models
{
    public partial class ErrorCode
    {
        public int Id { get; set; }
        public string? Code { get; set; }
        public string? Message { get; set; }
        public string? Description { get; set; }
    }
}
