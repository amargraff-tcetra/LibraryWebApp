using Dapper;
using LibraryApi.Models;
using Microsoft.Data.SqlClient;
using System.Data;

namespace LibraryApi.Repository
{
    public class BookRepository : IRepository<Book>
    {
        IDbConnection _connection;        
        public static string INSERT_BOOK = "INSERT INTO book (author_id, title, publisher, publication_date, paperback, copies) VALUES (@author_id, @title, @publisher, @publication_date, @paperback, @copies); SELECT SCOPE_IDENTITY();";
        public static string SELECT_BOOK = "SELECT * FROM book b JOIN author a ON b.author_id = a.id";
        public static string SELECT_BOOK_KEY = "SELECT * FROM book b JOIN author a ON b.author_id = a.id WHERE b.title LIKE '%' + @key + '%'";

        public BookRepository(IConfiguration configuration)
        {
            _connection = new SqlConnection(configuration.GetSection("DB_CONNECTION_STRING").Value);
        }

        public async Task<int> AddAsync(Book book)
        {
            return await _connection.ExecuteAsync(INSERT_BOOK, book);
        }

        public async Task<List<Book>> GetAllAsync()
        {
            var books = await _connection.QueryAsync<Book, Author, Book>(SELECT_BOOK, (b, a) => { b.author = a; return b; });
            return books.ToList();
        }

        public async Task<List<Book>> GetAllAsync(string key)
        {
            IEnumerable<Book> books = new List<Book>();
            var parameters = new DynamicParameters();
            parameters.Add("@key", key);
            books = await _connection.QueryAsync<Book, Author, Book>(SELECT_BOOK_KEY, (b, a) => { b.author = a; return b; }, parameters);

            return books.ToList();
        }

        public async Task<Book> GetAsync(int id)
        {
            IEnumerable<Book> books = new List<Book>();
            var sql = "SELECT * FROM book b JOIN author a ON b.author_id = a.id WHERE b.id = @id";
            var parameters = new DynamicParameters();
            parameters.Add("@id", id);
            books = await _connection.QueryAsync<Book, Author, Book>(sql, (b, a) => { b.author = a; return b; }, parameters);

            return books.FirstOrDefault() ?? new Book();
        }

        public Task<bool> UpdateAsync(Book entity)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
