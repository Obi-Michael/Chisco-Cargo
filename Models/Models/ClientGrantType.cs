﻿using System;
using System.Collections.Generic;
namespace Models.Models
{
    public partial class ClientGrantType
    {
        public int Id { get; set; }
        public string GrantType { get; set; } = null!;
        public int ClientId { get; set; }
    }
}
