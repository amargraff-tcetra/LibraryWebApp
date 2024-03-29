﻿using Dapper;
using LibraryApi.Models;
using Microsoft.Data.SqlClient;
using System.Data;

namespace LibraryApi.Repository
{
    public class BookRepository : IRepository<Book>
    {
        IDbConnection _connection;
        private static string INSERT_BOOK = "INSERT INTO book (author_id, title, publisher, publication_date, paperback, copies) VALUES (@author_id, @title, @publisher, @publication_date, @paperback, @copies); SELECT SCOPE_IDENTITY();";
        private static string UPDATE_BOOK = "UPDATE book SET author_id = @author_id, title = @title, publisher = @publisher, publication_date = @publication_date, paperback = @paperback, copies = @copies WHERE id = @id";
        private static string SELECT_BOOK_BY_ID = "SELECT * FROM book b JOIN author a ON b.author_id = a.id WHERE b.id = @id";
        private static string DELETE_BOOK_BY_ID = "DELETE FROM book WHERE id = @id";

        public BookRepository(IConfiguration configuration)
        {
            _connection = new SqlConnection(configuration.GetSection("DB_CONNECTION_STRING").Value);
        }


        public async Task<int> AddAsync(Book book)
        {
            return await _connection.ExecuteAsync(INSERT_BOOK, book);
        }

        public async Task<List<Book>> GetAllAsync()
        {
            return await GetAllAsync(string.Empty, string.Empty, string.Empty);
        }

        public async Task<List<Book>> GetAllAsync(string key)
        {
            return await GetAllAsync(key, string.Empty, string.Empty);
        }

        public async Task<List<Book>> GetAllAsync(string? title_key, string? author_name_key, string? publisher_key)
        {
            IEnumerable<Book> books = new List<Book>();
            var parameters = new DynamicParameters();
            parameters.Add("@title_key", title_key);
            parameters.Add("@author_name_key", author_name_key);
            parameters.Add("@publisher_key", publisher_key);
            books = await _connection.QueryAsync<Book, Author, Book>("usp_select_books", (b, a) => { b.author = a; return b; }, parameters, commandType: CommandType.StoredProcedure);

            return books.ToList();
        }

        public async Task<Book> GetAsync(int id)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@id", id);
            var book = (await _connection.QueryAsync<Book, Author, Book>(SELECT_BOOK_BY_ID, (b, a) => { b.author = a; return b; }, parameters)).FirstOrDefault() ?? new Book();
            return book;
        }

        public async Task<bool> UpdateAsync(Book book)
        {
            var result = await _connection.ExecuteAsync(UPDATE_BOOK, book);
            return result > 0;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@id", id);
            var result = await _connection.ExecuteAsync(DELETE_BOOK_BY_ID, parameters);
            return result > 0;
        }
    }
}
