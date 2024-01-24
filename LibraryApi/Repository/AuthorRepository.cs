using Dapper;
using LibraryApi.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace LibraryApi.Repository
{
    public class AuthorRepository : IRepository<Author>
    {
        IDbConnection _connection;
        private readonly LibraryContext _context;

        private static string SELECT_AUTHORS = "SELECT a.*, b.* FROM author a JOIN book b ON a.id = b.author_id";

        public AuthorRepository(LibraryContext context, IConfiguration configuration)
        {
            _context = context;
            _connection = new SqlConnection(configuration.GetSection("DB_CONNECTION_STRING").Value);
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

            var dapper_authors = await _connection.QueryAsync<Author, Book, Author>(SELECT_AUTHORS, (a, b) => {a.books.Add(b); return a; }, splitOn: "author_id");

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
