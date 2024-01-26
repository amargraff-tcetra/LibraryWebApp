using LibraryApi.Models;
using LibraryApi.Repository;
using Microsoft.AspNetCore.Mvc;


namespace LibraryApi.Services
{
    public class BookService: IService<Book>
    {
        private BookRepository _bookRepository { get; set; }
        private ILogger _logger { get; set; }


        public BookService(BookRepository bookRepository, ILogger<BookService> logger)
        {
            _bookRepository = bookRepository;
            _logger = logger;
        }

        public async Task<List<Book>> GetAsync()
        {
            List<Book> books = new List<Book>();
            try
            {
                books = await _bookRepository.GetAllAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Book Service GetAsync Exception: {ex.Message}");
            }
            return books;
        }

        public async Task<List<Book>> GetAsync(string key)
        {
            List<Book> books = new List<Book>();
            try
            {
                books = await _bookRepository.GetAllAsync(key);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Book Service GetAsync Exception: {ex.Message}");
            }
            return books;
        }        
        
        public async Task<List<Book>> GetAsync(string? title_key, string? author_name_key, string? publisher_key)
        {
            List<Book> books = new List<Book>();
            try
            {
                books = await _bookRepository.GetAllAsync(title_key, author_name_key, publisher_key);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Book Service GetAsync Exception: {ex.Message}");
            }
            return books;
        }

        public async Task<Book> GetAsync(int id)
        {
            Book book = new Book();
            try
            {
                book = await _bookRepository.GetAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Book Service GetAsync Exception: {ex.Message}");
            }
            return book;
        }

        //ADD BOOK
        public async Task<int> PostAsync(Book entity)
        {
            var result = 0;
            try
            {
                result = await _bookRepository.AddAsync(entity);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Book Service PostAsync Exception: {ex.Message}");
            }

            return result;
        }

        public async Task<bool> PutAsync(Book book)
        {
            bool result = false;
            try
            {
                result = await _bookRepository.UpdateAsync(book);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Book Service PutAsync Exception: {ex.Message}");
            }

            return false;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            bool result = false;
            try
            {
                result = await _bookRepository.DeleteAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Book Service DeleteAsync Exception: {ex.Message}");
            }

            return result;
        }
    }
}
