using Dapper;
using LibraryApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Collections;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace LibraryApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        // GET: api/books
        [HttpGet]
        public async Task<List<Book>> Get([FromQuery] string? key)
        {
            var connectionString = Environment.GetEnvironmentVariable("DB_CONNECTION_STRING") ?? throw new ArgumentException("Missing DB_CONNECTION_STRING environment variable");
            var db = new SqlConnection(connectionString);

            var sql = string.IsNullOrWhiteSpace(key) 
                ? "SELECT * FROM book b JOIN author a ON b.author_id = a.id" 
                : "SELECT * FROM book b JOIN author a ON b.author_id = a.id WHERE b.title LIKE '%' + @key +'%'";
            var parameters = new DynamicParameters();
            parameters.Add("@key", key);

            var books = await db.QueryAsync<Book, Author, Book>(sql, (b, a) => {b.author = a; return b;}, parameters);

            return books.ToList();

            //return new List<string>
            //{
            //    "The Hobbit",
            //    "Dune",
            //    "Harry Potter and the Sourecer's Stone",
            //    "The Lion, Witch and the Wordrobe",
            //    "The Lord of the Rings"
            //};
        }

        // GET api/<BooksController>/5
        [HttpGet("{id}")]
        public string Get()
        {
            return "value";
        }

        // POST api/<BooksController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<BooksController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<BooksController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
