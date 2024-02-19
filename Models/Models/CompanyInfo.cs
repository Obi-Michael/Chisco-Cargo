using System;
using System.Collections.Generic;

namespace Models.Models
{
    public partial class CompanyInfo
    {
        public int Id { get; set; }
        public DateTime CreationTime { get; set; }
        public long? CreatorUserId { get; set; }
        public DateTime? LastModificationTime { get; set; }
        public long? LastModifierUserId { get; set; }
        public bool IsDeleted { get; set; }
        public long? DeleterUserId { get; set; }
        public DateTime? DeletionTime { get; set; }
        public string? Name { get; set; }
        public string? Address { get; set; }
        public string? Email { get; set; }
        public string? ContactPerson { get; set; }
        public string? ContactName { get; set; }
        public DateTime PilotPayrollDate { get; set; }
        public int VatType { get; set; }
        public decimal TransportVat { get; set; }
        public string? BranchedFrom { get; set; }
        public bool IsParentCompany { get; set; }
        public DateTime? IsParentDate { get; set; }
        public int Days { get; set; }
    }
}
