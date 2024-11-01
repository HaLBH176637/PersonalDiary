namespace PersonalDiaryAPI.Models
{
    public class EditPostDTO
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Content { get; set; } = null!;
        public string Privacy { get; set; } = null!;
    }
}
