using System;
using System.Collections.Generic;

namespace Models.Models
{
    public partial class Bank
    {
        public int Id { get; set; }
        public string? AccountType { get; set; }
        public string? AccountNumber { get; set; }
        public string? AccountName { get; set; }
        public string? Name { get; set; }
    }
}
