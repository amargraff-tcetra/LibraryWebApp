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
        public async Task<List<Author>> Get()
        {
            return await _authorService.GetAsync();
        }

        // GET api/<AuthorController>/5
        [HttpGet("{id}")]
        public async Task<Author> Get(int id)
        {
            return await _authorService.GetAsync(id); ;
        }

        // POST api/<AuthorController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<AuthorController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<AuthorController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
