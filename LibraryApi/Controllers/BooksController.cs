using LibraryApi.Models;
using LibraryApi.Repository;
using LibraryApi.Services;
using Microsoft.AspNetCore.Mvc;
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
        private IService<Book> _bookService { get; set; }
        private ILogger _logger { get; set; }

        public BooksController(IService<Book> bookService, ILogger<BooksController> logger)
        {
            _bookService = bookService;
            _logger = logger;
        }

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
                books = await _bookService.GetAsync(key ?? string.Empty);
            }
            
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
