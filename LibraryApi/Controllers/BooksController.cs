using LibraryApi.Models;
using LibraryApi.Repository;
using LibraryApi.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json;
using Refit;
using System.Net;
using System.Net.Http;

namespace LibraryApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private BookService _bookService { get; set; }
        private ILogger _logger { get; set; }

        public BooksController(BookService bookService, ILogger<BooksController> logger)
        {
            _bookService = bookService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<List<Book>> Get([FromQuery] string? title_key, string? author_name_key, string? publisher_key)
        {
            var books = await _bookService.GetAsync(title_key, author_name_key, publisher_key);
            return books ?? new List<Book>();
        }

        [HttpGet("{id}")]
        public async Task<Book> Get(int id)
        {
            return await _bookService.GetAsync(id);
        }

        [HttpPost]
        public async Task<int> Post([FromBody] Book book)
        {
            var result = 0;
            try
            {
                result = await _bookService.PostAsync(book);
            }catch (Exception ex)
            {
                _logger.LogError($"Exception: {ex.Message}");
            }

            return result;
        }

        [HttpPut("{id}")]
        public async Task<bool> Put([FromBody] Book book)
        {
            var result = false;
            try
            {
                result = await _bookService.PutAsync(book);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception: {ex.Message}");
            }

            return result;
        }

        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            await _bookService.DeleteAsync(id);
        }
    }
}
