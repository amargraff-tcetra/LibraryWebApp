using LibraryApi.Models;
using LibraryApi.Repository;
using Microsoft.AspNetCore.Mvc;


namespace LibraryApi.Services
{
    public class BookService: IService<Book>
    {
        private IRepository<Book> _bookRepository { get; set; }
        private ILogger _logger { get; set; }


        public BookService(IRepository<Book> bookRepository, ILogger<BookService> logger)
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

        public async Task<int> PostAsync(Book entity)
        {
            try
            {
                throw new NotImplementedException();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Book Service PostAsync Exception: {ex.Message}");
            }

            return 0;
        }

        public async Task<bool> PutAsync(Book entity)
        {
            try
            {
                throw new NotImplementedException();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Book Service PutAsync Exception: {ex.Message}");
            }

            return false;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                throw new NotImplementedException();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Book Service DeleteAsync Exception: {ex.Message}");
            }

            return false;
        }
    }
}
