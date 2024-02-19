﻿using System;
using System.Collections.Generic;

namespace Models.Models
{
    public partial class AgentWallet
    {
        public int Id { get; set; }
        public DateTime CreationTime { get; set; }
        public long? CreatorUserId { get; set; }
        public DateTime? LastModificationTime { get; set; }
        public long? LastModifierUserId { get; set; }
        public bool IsDeleted { get; set; }
        public long? DeleterUserId { get; set; }
        public DateTime? DeletionTime { get; set; }
        public int AgentId { get; set; }
        public decimal Amount { get; set; }
    }
}
