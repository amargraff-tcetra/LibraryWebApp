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
        private static string SELECT_KEY_AUTHORS = "SELECT a.*, b.* FROM author a JOIN book b ON a.id = b.author_id WHERE a.first_name LIKE '%' + @key + '%' OR a.last_name LIKE '%' + @key + '%'";

        public AuthorRepository(LibraryContext context, IConfiguration configuration)
        {
            _context = context;
            _connection = new SqlConnection(configuration.GetSection("DB_CONNECTION_STRING").Value);
        }

        public async Task<int> AddAsync(Author author)
        {
            _context.Authors.Add(author);
            await _context.SaveChangesAsync();
            return author.id;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var author = await _context.Authors.FirstOrDefaultAsync(a => a.id == id);
            if(author != null)
            {
                _context.Authors.Remove(author);
            }

            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<List<Author>> GetAllAsync()
        {
            //DAPPER METHOD
            var dict = new Dictionary<int, Author>();
            var dapper_authors = (await _connection.QueryAsync<Author, Book, Author>(SELECT_AUTHORS, (a, b) => MapAuthorBooks(dict,a,b)))?.Distinct().ToList() ?? new List<Author>();

            //EF METHOD
            return await _context.Authors
                .Include(a => a.books)
                .ToListAsync();
        }

        public async Task<List<Author>> GetAllAsync(string key)
        {
            //DAPPER METHOD
            var parameters = new DynamicParameters();
            parameters.Add("@key", key);
            var dict = new Dictionary<int,Author>();
            var dapper_authors = (await _connection.QueryAsync<Author, Book, Author>(SELECT_KEY_AUTHORS, (a, b) => MapAuthorBooks(dict,a,b), parameters))?.Distinct().ToList() ?? new List<Author>();

            //EF METHOD
            return await _context.Authors
                .Include(a => a.books)
                .Where(a => EF.Functions.Like(a.first_name, $"%{key}%") || EF.Functions.Like(a.last_name, $"%{key}%"))
                .ToListAsync();
        }

        public async Task<Author> GetAsync(int id)
        {
            return await _context.Authors
                .Include(a => a.books)
                .Where(a => a.id == id)
                .FirstOrDefaultAsync() ?? new Author();
        }

        public async Task<bool> UpdateAsync(Author author)
        {
            var authorToUpdate = await _context.Authors.FirstOrDefaultAsync(a => a.id == author.id);
            if (authorToUpdate != null)
            {
                authorToUpdate.first_name = author.first_name;
                authorToUpdate.last_name = author.last_name;
                authorToUpdate.date_of_birth = author.date_of_birth;
                return await _context.SaveChangesAsync() > 0;
            }

            return false;
        }


        //DAPPER HELPER METHOD
        private Author MapAuthorBooks(Dictionary<int, Author> dict, Author a, Book b)
        {
            if (!dict.TryGetValue(a.id, out var thisAuthor))
            {
                thisAuthor = a;
                thisAuthor.books = new List<Book>();
                dict[a.id] = a;
            }

            thisAuthor.books.Add(b);
            return thisAuthor;
        }
    }
}
