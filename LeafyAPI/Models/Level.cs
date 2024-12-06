using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LeafyAPI.Models
{
    public class Level
    {
        [Key]
        public int Id { get; set; }

        [Range(1, int.MaxValue)]
        public int CurrentLevel { get; set; }

        [Range(0, int.MaxValue)]
        public int ExperiencePoints { get; set; }

        public int ExperienceThreshold { get; set; }

        public required string UserId { get; set; }

        [ForeignKey("UserId")]
        public User User { get; set; } = null!;
    }   
}