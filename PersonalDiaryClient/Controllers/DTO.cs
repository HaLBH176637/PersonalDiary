namespace PersonalDiaryClient.Models
{
    public class UserDTO
    {
        public string Username { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string Fullname { get; set; } = null!;
        public DateTime? Dob { get; set; }
        public string? Number { get; set; }
        public DateTime? CreatedAt { get; set; }
        public bool? IsBlock { get; set; }
        public int? RoleId { get; set; }
        public string? PrivatePassword { get; set; }
    }
    public class PostDTO
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Content { get; set; } = null!;
        public string Tag { get; set; } = null!;
        public DateTime? CreatedAt { get; set; }
        public string Privacy { get; set; } = null!;

        public string Fullname { get; set; } = null!;
    }
    public class NewPostDTO
    {
        public int UserId { get; set; }
        public string Tag { get; set; } = null!;
        public string Content { get; set; } = null!;
        public DateTime? CreatedAt { get; set; }
        public string Privacy { get; set; } = null!;
    }
    public class EditPostDTO
    {
        public int UserId { get; set; }
        public string Tag { get; set; } = null!;
        public string Content { get; set; } = null!;
        public DateTime? CreatedAt { get; set; }
        public string Privacy { get; set; } = null!;
    }
    public partial class ReportDTO
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int PostId { get; set; }
        public string? Reason { get; set; }
        public DateTime? CreatedAt { get; set; }

    }

}
