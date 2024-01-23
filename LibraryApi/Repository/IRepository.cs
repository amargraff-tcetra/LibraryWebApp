﻿namespace LibraryApi.Repository
{
    public interface IRepository<T> where T: class
    {
        Task<List<T>> GetAllAsync();
        Task<List<T>> GetAllAsync(string key);
        Task<T> GetAsync(int id);
        Task<int> AddAsync(T entity);
        Task<bool> UpdateAsync(T entity);
        Task<bool> DeleteAsync(int id);
    }
}
