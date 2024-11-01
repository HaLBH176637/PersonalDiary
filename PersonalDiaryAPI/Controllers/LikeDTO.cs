namespace PersonalDiaryAPI.Models
{
    public class LikeDTO
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int PostId { get; set; }
        public DateTime? CreatedAt { get; set; }
    }
}
