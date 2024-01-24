using LibraryWebApp.Models;
using Refit;

namespace LibraryWebApp.Abstraction
{
    public interface IAuthorClient
    {
        [Get("/api/authors")]
        Task<List<Author>> Get(string? key);

        [Get("/api/authors/{id}")]
        Task<Author> Get(int id);

        [Post("/api/authors")]
        Task Post(Author author);

        [Put("/api/authors/{id}")]
        Task Put(int id, Author author);

        [Delete("/api/authors/{id}")]
        Task Delete(int id);
    }
}
