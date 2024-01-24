using LibraryApi.Models;
using LibraryApi.Repository;

namespace LibraryApi.Services
{
    public class AuthorService : IService<Author>
    {
        private readonly IRepository<Author> _authorRepository;

        public AuthorService(IRepository<Author> authorRepository)
        {
            _authorRepository = authorRepository;
        }

        public async Task<List<Author>> GetAsync()
        {
           return await _authorRepository.GetAllAsync();
        }

        public async Task<List<Author>> GetAsync(string key)
        {
            return await _authorRepository.GetAllAsync(key);
        }

        public async Task<Author> GetAsync(int id)
        {
            return await _authorRepository.GetAsync(id);
        }

        public async Task<int> PostAsync(Author author)
        {
            return await _authorRepository.AddAsync(author);
        }

        public async Task<bool> PutAsync(Author author)
        {
            return await _authorRepository.UpdateAsync(author);
        }
        public Task<bool> DeleteAsync(int id)
        {
            return _authorRepository.DeleteAsync(id);
        }
    }
}
