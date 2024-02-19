﻿namespace Models.Models
{
    public partial class JobQueue
    {
        public int Id { get; set; }
        public long JobId { get; set; }
        public string Queue { get; set; } = null!;
        public DateTime? FetchedAt { get; set; }
    }
}
