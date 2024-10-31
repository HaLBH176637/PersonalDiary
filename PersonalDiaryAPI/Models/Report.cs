using System;
using System.Collections.Generic;

namespace PersonalDiaryAPI.Models
{
    public partial class Report
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int PostId { get; set; }
        public string? Reason { get; set; }
        public DateTime? CreatedAt { get; set; }

        public virtual Post Post { get; set; } = null!;
        public virtual User User { get; set; } = null!;
    }
}
