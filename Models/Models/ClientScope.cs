using System;
using System.Collections.Generic;

namespace Models.Models
{
    public partial class ClientScope
    {
        public int Id { get; set; }
        public string Scope { get; set; } = null!;
        public int ClientId { get; set; }
    }
}
