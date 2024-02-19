using System;
using System.Collections.Generic;

namespace Models.Models
{
    public partial class ApiScopeClaim
    {
        public int Id { get; set; }
        public string Type { get; set; } = null!;
        public int ApiScopeId { get; set; }
    }
}
