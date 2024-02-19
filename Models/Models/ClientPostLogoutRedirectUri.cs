using System;
using System.Collections.Generic;

namespace Models.Models
{
    public partial class ClientPostLogoutRedirectUri
    {
        public int Id { get; set; }
        public string PostLogoutRedirectUri { get; set; } = null!;
        public int ClientId { get; set; }
    }
}
