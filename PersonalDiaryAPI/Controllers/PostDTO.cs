using PersonalDiaryAPI.Models;

namespace PersonalDiaryAPI.Controllers
{
    public class PostDTO
    {
        public PostDTO()
        {
            Likes = new HashSet<Like>();
            Reports = new HashSet<Report>();
            Shares = new HashSet<Share>();
        }

        public int Id { get; set; }
        public int UserId { get; set; }
        public string Content { get; set; } = null!;
        public string Tag { get; set; } = null!;
        public DateTime? CreatedAt { get; set; }
        public string Privacy { get; set; } = null!;

        public virtual User User { get; set; } = null!;
        public virtual ICollection<Like> Likes { get; set; }
        public virtual ICollection<Report> Reports { get; set; }
        public virtual ICollection<Share> Shares { get; set; }
    }
    public partial class LikeDTO
    {

        public int UserId { get; set; }
        public DateTime? CreatedAt { get; set; }

    }
}
