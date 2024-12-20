using System.ComponentModel.DataAnnotations;

namespace LeafyAPI.DTOs.User
{
    public class UpdateUserInfoRequestDto
    {
        [Required]
        [StringLength(20, MinimumLength = 3)]
        public required string Name { get; set; }
    }
}
