﻿using System;
using System.Collections.Generic;

namespace Models.Models
{
    public partial class ClientCorsOrigin
    {
        public int Id { get; set; }
        public string Origin { get; set; } = null!;
        public int ClientId { get; set; }
    }
}
