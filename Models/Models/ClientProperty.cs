﻿using System;
using System.Collections.Generic;

namespace Models.Models
{
    public partial class ClientProperty
    {
        public int Id { get; set; }
        public string Key { get; set; } = null!;
        public string Value { get; set; } = null!;
        public int ClientId { get; set; }
    }
}
