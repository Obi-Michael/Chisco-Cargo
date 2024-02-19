using System;
using System.Collections.Generic;

namespace Models.Models
{
    public partial class ApiProperty
    {
        public int Id { get; set; }
        public string Key { get; set; } = null!;
        public string Value { get; set; } = null!;
        public int ApiResourceId { get; set; }
    }
}
