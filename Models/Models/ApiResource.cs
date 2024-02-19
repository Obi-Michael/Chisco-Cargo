using System;
using System.Collections.Generic;

namespace Models.Models
{
    public partial class ApiResource
    {
        public int Id { get; set; }
        public bool Enabled { get; set; }
        public string Name { get; set; } = null!;
        public string? DisplayName { get; set; }
        public string? Description { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Updated { get; set; }
        public DateTime? LastAccessed { get; set; }
        public bool NonEditable { get; set; }
    }
}
