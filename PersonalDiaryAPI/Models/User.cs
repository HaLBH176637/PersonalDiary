using System;
using System.Collections.Generic;

namespace PersonalDiaryAPI.Models
{
    public partial class User
    {
        public User()
        {
            Likes = new HashSet<Like>();
            Posts = new HashSet<Post>();
            Reports = new HashSet<Report>();
            Shares = new HashSet<Share>();
        }

        public int Id { get; set; }
        public string Username { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string Fullname { get; set; } = null!;
        public DateTime? Dob { get; set; }
        public string? Number { get; set; }
        public DateTime? CreatedAt { get; set; }
        public bool? IsBlock { get; set; }
        public int? RoleId { get; set; }

        public virtual Role? Role { get; set; }
        public virtual ICollection<Like> Likes { get; set; }
        public virtual ICollection<Post> Posts { get; set; }
        public virtual ICollection<Report> Reports { get; set; }
        public virtual ICollection<Share> Shares { get; set; }
    }
}
