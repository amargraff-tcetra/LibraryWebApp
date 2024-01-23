using Microsoft.AspNetCore.Mvc;
using Refit;

namespace LibraryWebApp.Models
{
    public interface IBookApi
    {
        [Get("/api/books")]
        Task<List<Book>> Get(string? key);

        [Get("/api/books/{id}")]
        Task<Book> Get(int id);

        [Post("/api/books")]
        Task Post(Book book);

        [Put("{id}")]
        Task Put(int id, Book book);

        [Delete("{id}")]
        Task Delete(int id);
    }
}
