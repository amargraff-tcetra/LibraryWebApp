using LibraryApi.Models;
using LibraryApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace LibraryApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        public BookService _bookService { get; set; }

        public BooksController()
        {
            _bookService = new BookService();
        }

        // GET: api/books
        [HttpGet]
        public async Task<List<Book>> Get([FromQuery] string? key)
        {
            var books = new List<Book>();
            if (string.IsNullOrWhiteSpace(key))
            {
                books = await _bookService.GetBooksAsync();
            }
            else
            {
                books = await _bookService.GetBooksAsync(key);
            }

            return books ?? new List<Book>();
        }

        // GET api/books/5
        [HttpGet("{id}")]
        public async Task<Book> Get(int id)
        {
            return await _bookService.GetBookAsync(id);
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
