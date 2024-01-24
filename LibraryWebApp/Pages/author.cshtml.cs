using LibraryWebApp.Abstraction;
using LibraryWebApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LibraryWebApp.Pages
{
    public class AuthorModel : PageModel
    {
        private ILogger _logger;
        private IAuthorClient _authorClient;

        public AuthorModel(ILogger<AuthorModel> logger, IAuthorClient authorClient)
        {
            _logger = logger;
            _authorClient = authorClient;
        }

        public void OnGet()
        {
        }

        public async void OnPostAuthor(string first_name, string last_name, DateTime date_of_birth) 
        {
            var author = new Author();
            author.first_name = first_name;
            author.last_name = last_name;
            author.date_of_birth = date_of_birth;

            var result = await _authorClient.Post(author);
        }
    }
}
