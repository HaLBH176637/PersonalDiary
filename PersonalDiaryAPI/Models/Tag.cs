using System;
using System.Collections.Generic;

namespace PersonalDiaryAPI.Models
{
    public partial class Tag
    {
        public Tag()
        {
            Posts = new HashSet<Post>();
        }

        public int Id { get; set; }
        public string TagName { get; set; } = null!;

        public virtual ICollection<Post> Posts { get; set; }
    }
}
