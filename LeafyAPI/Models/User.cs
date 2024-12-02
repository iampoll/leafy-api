using Microsoft.AspNetCore.Identity;

namespace LeafyAPI.Models
{
    public class User : IdentityUser
    {
        public bool isOnboarded { get; set; }
    }
}