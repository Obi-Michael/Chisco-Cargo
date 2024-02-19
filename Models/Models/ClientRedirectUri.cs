using System;
using System.Collections.Generic;

namespace Models.Models
{
    public partial class ClientRedirectUri
    {
        public int Id { get; set; }
        public string RedirectUri { get; set; } = null!;
        public int ClientId { get; set; }
    }
}
