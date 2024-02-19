using System;
using System.Collections.Generic;

namespace Models.Models
{
    public partial class ApiClaim
    {
        public int Id { get; set; }
        public string Type { get; set; } = null!;
        public int ApiResourceId { get; set; }
    }
}
