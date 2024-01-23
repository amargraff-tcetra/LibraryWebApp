namespace LibraryApi.Services
{
    public interface IService<T> where T : class
    {
        Task<List<T>> GetAsync();
        Task<List<T>> GetAsync(string key);
        Task<T> GetAsync(int id);
        Task<int> PostAsync(T entity);
        Task<bool> PutAsync(T entity);
        Task<bool> DeleteAsync(int id);
    }
}
