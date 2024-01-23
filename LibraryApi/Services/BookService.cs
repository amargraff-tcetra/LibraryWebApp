using LibraryApi.Models;
using LibraryApi.Repository;


namespace LibraryApi.Services
{
    public class BookService
    {

        public async Task<List<Book>> GetBooksAsync()
        {
            List<Book> books = new List<Book>();
            try
            {
                BookRepository bookRepository = new BookRepository();
                books = await bookRepository.GetAllAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Book Service GetBooksAsync Exception: {ex.Message}");
            }
            return books;
        }

        public async Task<List<Book>> GetBooksAsync(string key)
        {
            List<Book> books = new List<Book>();
            try
            {
                BookRepository bookRepository = new BookRepository();
                books = await bookRepository.GetAllAsync(key);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Book Service GetBooksAsync Exception: {ex.Message}");
            }
            return books;
        }

        public async Task<Book> GetBookAsync(int id)
        {
            Book book = new Book();
            try
            {
                BookRepository bookRepository = new BookRepository();
                book = await bookRepository.GetAsync(id);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Book Service GetBookAsync Exception: {ex.Message}");
            }
            return book;
        }
    }
}
