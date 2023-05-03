using ElearnFrontToBack.Data;
using ElearnFrontToBack.Models;
using ElearnFrontToBack.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ElearnFrontToBack.Services
{
    public class AuthorService : IAuthorService
    {
        private readonly AppDbContext _context;

        public AuthorService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Author>> GetAll()
        {
            return await _context.Authors.ToListAsync();
        }
    }
}
