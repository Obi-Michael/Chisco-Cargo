using System;
using System.Collections.Generic;

namespace Models.Models
{
    public partial class ClientIdPrestriction
    {
        public int Id { get; set; }
        public string Provider { get; set; } = null!;
        public int ClientId { get; set; }
    }
}
