namespace PersonalDiaryAPI.Controllers
{
    public class UserDTO
    {
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

    }
}
