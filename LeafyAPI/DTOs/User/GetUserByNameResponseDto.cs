using LeafyAPI.Models;

namespace LeafyAPI.DTOs.User
{
    public class GetUserByNameResponseDto
    {
        public string Name { get; set; } = string.Empty;
        public LevelResponseDto Level { get; set; } = null!;
    }
}
