namespace LeafyAPI.DTOs.User
{
    public class GetUserResponseDto
    {
        public string Id { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public bool IsOnboarded { get; set; }
        public string Name { get; set; } = string.Empty;
        public string NameSlug { get; set; } = string.Empty;
    }
}
