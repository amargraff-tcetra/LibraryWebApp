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
        public List<Author> Authors { get; set; } = new List<Author>();
        [BindProperty]
        public Author SelectedAuthor { get; set; } = new Author();
        public string? SearchedFirstName { get; set; }
        public string? SearchedLastName { get; set; }
        public bool AddAuthor { get; set; } = false;
        public bool UpdateAuthor { get; set; } = false;

        public AuthorModel(ILogger<AuthorModel> logger, IAuthorClient authorClient)
        {
            _logger = logger;
            _authorClient = authorClient;
        }

        public void OnGet()
        {
            UpdateAuthor = false;
            AddAuthor = false;
        }

        public async Task<IActionResult> OnPostSearchAuthor(string first_name, string last_name)
        {
            SearchedFirstName = first_name;
            SearchedLastName = last_name;

            Authors.Clear();
            if (!string.IsNullOrEmpty(last_name)) 
            {
                Authors.AddRange((await _authorClient.Get(last_name)).ToList());
            }

            if (!string.IsNullOrEmpty(first_name))
            {
                var theseAuthors = await _authorClient.Get(first_name) ?? new List<Author>();
                theseAuthors.RemoveAll(a => Authors.Select(x => x.id).Contains(a.id));
                Authors.AddRange(theseAuthors);
            }

            Authors = Authors.Distinct().ToList();
            if (!Authors.Any()) 
            {
                AddAuthor = true;
            }

            return Page();
        }


        public async void OnPostAddAuthor(string first_name, string last_name, DateTime date_of_birth) 
        {
            var author = new Author();
            int result = 0;
            if (!string.IsNullOrEmpty(first_name) && !string.IsNullOrEmpty(last_name) && date_of_birth != default) 
            {
                author.first_name = first_name;
                author.last_name = last_name;
                author.date_of_birth = date_of_birth;
                result = await _authorClient.Post(author);
            }
        }

        public async void OnPostUpdateAuthor(int id,string first_name, string last_name, DateTime date_of_birth) 
        {
            var updated_author = new Author()
            {
                id = id,
                first_name = first_name,
                last_name = last_name,
                date_of_birth = date_of_birth
            };

            var result = await _authorClient.Put(id, updated_author);
        }

        public IActionResult OnPostEditAuthor(int id, string first_name, string last_name, DateTime date_of_birth)
        {
            UpdateAuthor = true;
            SelectedAuthor = new Author()
            {
                id = id,
                first_name = first_name,
                last_name = last_name,
                date_of_birth = date_of_birth
            };

            return Page();
        }

        public IActionResult OnPostStartAddAuthor(string first_name, string last_name)
        {
            AddAuthor = true;
            SearchedFirstName = first_name;
            SearchedLastName = last_name;

            return Page();
        }
    }
}
