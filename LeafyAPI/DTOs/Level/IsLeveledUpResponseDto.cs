namespace LeafyAPI.DTOs
{
    public class IsLeveledUpResponseDto
    {
        public required bool IsLeveledUp { get; set; }
        public required LevelResponseDto Level { get; set; }
    }
} 