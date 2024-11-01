namespace PersonalDiaryAPI.Models
{
    public class CreatePostDTO
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Content { get; set; } = null!;
        public DateTime? CreatedAt { get; set; }
        public string Privacy { get; set; } = null!;
    }
}
