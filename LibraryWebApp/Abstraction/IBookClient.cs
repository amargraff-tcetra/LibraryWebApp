using LibraryWebApp.Models;
using Refit;

namespace LibraryWebApp.Abstraction
{
    public interface IBookClient
    {
        [Get("/api/books")]
        Task<List<Book>> Get(string? key);

        [Get("/api/books/{id}")]
        Task<Book> Get(int id);

        [Post("/api/books")]
        Task Post(Book book);

        [Put("/api/books/{id}")]
        Task Put(int id, Book book);

        [Delete("/api/books/{id}")]
        Task Delete(int id);
    }
}
