using System;
using System.Collections.Generic;

namespace PersonalDiaryClient.Models
{
    public partial class Post
    {
        public Post()
        {
            Likes = new HashSet<Like>();
            Reports = new HashSet<Report>();
            Shares = new HashSet<Share>();
            Tags = new HashSet<Tag>();
        }

        public int Id { get; set; }
        public int UserId { get; set; }
        public string Content { get; set; } = null!;
        public DateTime? CreatedAt { get; set; }
        public string Privacy { get; set; } = null!;

        public virtual User User { get; set; } = null!;
        public virtual ICollection<Like> Likes { get; set; }
        public virtual ICollection<Report> Reports { get; set; }
        public virtual ICollection<Share> Shares { get; set; }

        public virtual ICollection<Tag> Tags { get; set; }
    }
}
