namespace LeafyAPI.DTOs
{
    public class CustomUserInfo
    {
        public required string Id { get; set; }
        public string Email { get; set; } = string.Empty;
        public bool IsOnboarded { get; set; }
    }
} 