using Dapper;
using LibraryApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace LibraryApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        // GET: api/books
        [HttpGet]
        public async Task<List<string>> Get()
        {
            //var db = new SqlConnection("Data Source=127.0.0.1,1433;Initial Catalog=MyLibrary;Persist Security Info=True;User ID=sa;Password=SQLserver123!;TrustServerCertificate=True");
            var db = new SqlConnection("Data Source=library_database,1433;Initial Catalog=MyLibrary;Persist Security Info=True;User ID=sa;Password=SQLserver123!;TrustServerCertificate=True");

            var books = await db.QueryAsync<Book>("SELECT * FROM book");

            return books.Select(b => b.title).ToList();

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
        public string Get(int id)
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
