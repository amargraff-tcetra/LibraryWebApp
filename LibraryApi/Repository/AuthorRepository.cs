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
            return await _context.Authors.Include(a => a.books).ToListAsync();
        }

        public async Task<List<Author>> GetAllAsync(string key)
        {
            return await _context.Authors.Include(a => a.books).Where(a => a.first_name.Contains(key, StringComparison.InvariantCultureIgnoreCase) || a.last_name.Contains(key, StringComparison.InvariantCultureIgnoreCase)).ToListAsync();
        }

        public async Task<Author> GetAsync(int id)
        {
            return await _context.Authors.Include(a => a.books).Where(a => a.id == id).FirstOrDefaultAsync() ?? new Author();
        }

        public async Task<bool> UpdateAsync(Author entity)
        {
            throw new NotImplementedException();
        }
    }
}
