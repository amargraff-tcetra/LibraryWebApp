using LibraryApi.Models;
using LibraryApi.Repository;
using LibraryApi.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http;

namespace LibraryApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private IService<Book> _bookService { get; set; }
        private ILogger _logger { get; set; }

        public BooksController(IService<Book> bookService, ILogger<BooksController> logger)
        {
            _bookService = bookService;
            _logger = logger;
        }

        // GET: api/books
        [HttpGet]
        public async Task<List<Book>> Get([FromQuery] string? key)
        {
            var books = new List<Book>();
            if (string.IsNullOrWhiteSpace(key))
            {
                books = await _bookService.GetAsync();
            }
            else
            {
                books = await _bookService.GetAsync(key);
            }

            return books ?? new List<Book>();
        }

        // GET api/books/5
        [HttpGet("{id}")]
        public async Task<Book> Get(int id)
        {
            return await _bookService.GetAsync(id);
        }

        // POST api/<BooksController>
        [HttpPost]
        public void Post([FromBody] Book book)
        {
            //Book? book = JsonConvert.DeserializeObject<Book>(value);

            if (book != null)
            {
                _logger.LogError("Could not deserialize body as Book object");
            }
        }

        // PUT api/<BooksController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] Book book)
        {
            //Book? book = JsonConvert.DeserializeObject<Book>(value);

            if (book != null)
            {
                _logger.LogError("Could not deserialize body as Book object");
            }
        }

        // DELETE api/<BooksController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _bookService.DeleteAsync(id);
        }
    }
}
