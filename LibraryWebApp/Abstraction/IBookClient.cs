using LibraryWebApp.Models;
using Refit;

namespace LibraryWebApp.Abstraction
{
    public interface IBookClient
    {
        [Get("/api/books")]
        Task<List<Book>> Get(string? title_key, string? author_name_key, string? publisher_key);

        [Get("/api/books/{id}")]
        Task<Book> Get(int id);

        [Post("/api/books")]
        Task<int> Post(Book book);

        [Put("/api/books/{id}")]
        Task<bool> Put(int id, Book book);

        [Delete("/api/books/{id}")]
        Task<bool> Delete(int id);
    }
}
