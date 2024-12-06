using Microsoft.AspNetCore.Identity;

namespace LeafyAPI.Models
{
    public class User : IdentityUser
    {
        public bool isOnboarded { get; set; }
        public string Name { get; set; } = string.Empty;
        public Level Level { get; set; } = null!;
    }
}