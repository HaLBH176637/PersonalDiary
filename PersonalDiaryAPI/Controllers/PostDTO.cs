using PersonalDiaryAPI.Models;

namespace PersonalDiaryAPI.Controllers
{
    public class PostDTO
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Content { get; set; } = null!;
        public string Tag { get; set; } = null!;
        public DateTime? CreatedAt { get; set; }
        public string Privacy { get; set; } = null!;

        public UserDTO User { get; set; } = null!;

        public virtual List<LikeDTO> Likes { get; set; }
        public virtual List<ReportDTO> Reports { get; set; }
        public virtual List<ShareDTO> Shares { get; set; }
    }
    public class NewPostDTO
    {
        public int UserId { get; set; }
        public string Tag { get; set; } = null!;
        public string Content { get; set; } = null!;
        public DateTime? CreatedAt { get; set; }
        public string Privacy { get; set; } = null!;
    }
    public partial class LikeDTO
    {

        public int Id { get; set; }
        public int UserId { get; set; }
        public int PostId { get; set; }
        public DateTime? CreatedAt { get; set; }

    }
    public partial class ShareDTO
    {
        public int Id { get; set; }
        public int PostId { get; set; }
        public int UserId { get; set; }
        public DateTime? CreatedAt { get; set; }
    }
    public partial class ReportDTO
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int PostId { get; set; }
        public string? Reason { get; set; }
        public DateTime? CreatedAt { get; set; }

    }
    public class EditPostDTO
    {
        public int UserId { get; set; }
        public string Tag { get; set; } = null!;
        public string Content { get; set; } = null!;
        public DateTime? CreatedAt { get; set; }
        public string Privacy { get; set; } = null!;
    }
}
