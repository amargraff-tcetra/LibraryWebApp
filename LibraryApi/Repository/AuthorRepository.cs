using LibraryApi.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryApi.Repository
{
    public class AuthorRepository : IRepository<Author>
    {
        private readonly LibraryContext _context;

        public AuthorRepository(LibraryContext context)
        {
            _context = context;
        }

        public async Task<int> AddAsync(Author entity)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Author>> GetAllAsync()
        {
            return await _context.Authors
                .Include(a => a.books)
                .ToListAsync();
        }

        public async Task<List<Author>> GetAllAsync(string key)
        {
            var authors = await _context.Authors
                .Include(a => a.books)
                .Where(a => EF.Functions.Like(a.first_name, $"%{key}%") || EF.Functions.Like(a.last_name, $"%{key}%"))
                .ToListAsync();

            return authors;
        }

        public async Task<Author> GetAsync(int id)
        {
            return await _context.Authors
                .Include(a => a.books)
                .Where(a => a.id == id)
                .FirstOrDefaultAsync() ?? new Author();
        }

        public async Task<bool> UpdateAsync(Author entity)
        {
            throw new NotImplementedException();
        }
    }
}
