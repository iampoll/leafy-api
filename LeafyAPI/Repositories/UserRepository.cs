using LeafyAPI.Data;
using LeafyAPI.Models;
using LeafyAPI.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LeafyAPI.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext _context;

        public UserRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<User?> GetUserByNameSlugAsync(string nameSlug)
        {
            return await _context.Users.Include(u => u.Level).FirstOrDefaultAsync(u => u.NameSlug == nameSlug);
        }
    }
}
