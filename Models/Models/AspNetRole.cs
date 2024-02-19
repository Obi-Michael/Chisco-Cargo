using System;
using System.Collections.Generic;

namespace Models.Models
{
    public partial class AspNetRole
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? NormalizedName { get; set; }
        public string? ConcurrencyStamp { get; set; }
        public bool IsActive { get; set; }
        public bool IsDefaultRole { get; set; }
        public DateTime? CreationTime { get; set; }
        public int CompanyInfoId { get; set; }
        public string? RolesDescription { get; set; }
    }
}
