namespace LeafyAPI.DTOs
{
    public class LeaderboardResponseDto
    {
        public string Name { get; set; } = string.Empty;
        public string NameSlug { get; set; } = string.Empty;
        public int CurrentLevel { get; set; }
        public int ExperiencePoints { get; set; }
        public int TotalExperiencePoints { get; set; }
        public int Rank { get; set; }
    }
} 