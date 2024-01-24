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

        public Task<bool> DeleteAsync(int id)
        {
            throw new NotImplementedException();
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

        public async Task<int> PostAsync(Author entity)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> PutAsync(Author entity)
        {
            throw new NotImplementedException();
        }
    }
}
