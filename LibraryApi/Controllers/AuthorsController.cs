using LibraryApi.Models;
using LibraryApi.Services;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace LibraryApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorsController : ControllerBase
    {
        private readonly IService<Author>  _authorService;
        private readonly ILogger _logger;

        public AuthorsController(IService<Author> authorService, ILogger<AuthorsController> logger)
        {
            _authorService = authorService;
            _logger = logger;
        }

        // GET: api/<AuthorController>
        [HttpGet]
        public async Task<List<Author>> Get([FromQuery]string? key)
        {
            var authors = new List<Author>();
            if (string.IsNullOrWhiteSpace(key))
            {
                authors = await _authorService.GetAsync();
            }
            else
            {
                authors = await _authorService.GetAsync(key);
            }
            return authors;
        }

        // GET api/<AuthorController>/5
        [HttpGet("{id}")]
        public async Task<Author> Get(int id)
        {
            return await _authorService.GetAsync(id); ;
        }

        // POST api/<AuthorController>
        [HttpPost]
        public async Task<int> Post([FromBody] Author author)
        {
           return await _authorService.PostAsync(author);
        }

        // PUT api/<AuthorController>/5
        [HttpPut("{id}")]
        public async Task<bool> Put(int id, [FromBody] Author author)
        {
            author.id = id;
            return await _authorService.PutAsync(author);
        }

        // DELETE api/<AuthorController>/5
        [HttpDelete("{id}")]
        public async Task<bool> Delete(int id)
        {
            var author = await _authorService.GetAsync(id);

            if (author.books.Any())
            {
                return false;
            }

            return await _authorService.DeleteAsync(id);
        }
    }
}
