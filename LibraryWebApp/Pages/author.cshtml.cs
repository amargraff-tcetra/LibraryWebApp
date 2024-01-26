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
        private IBookClient _bookClient;
        public List<Author> Authors { get; set; } = new List<Author>();
        [BindProperty]
        public Author SelectedAuthor { get; set; } = new Author();
        public string? SearchedFirstName { get; set; } = string.Empty;
        public string? SearchedLastName { get; set; } = string.Empty;
        public bool AddAuthor { get; set; } = false;
        public bool UpdateAuthor { get; set; } = false;
        public bool ShowError { get; set; } = false;

        public AuthorModel(ILogger<AuthorModel> logger, IAuthorClient authorClient, IBookClient bookClient)
        {
            _logger = logger;
            _authorClient = authorClient;
            _bookClient = bookClient;
        }

        public void OnGet()
        {
            ShowError = false;
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
            AddAuthor = false;
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
            UpdateAuthor = false;
        }

        public async Task<IActionResult> OnPostEditAuthor(int id, string first_name, string last_name, DateTime date_of_birth)
        {
            UpdateAuthor = true;
            SelectedAuthor = await _authorClient.Get(id);

            return Page();
        }

        public IActionResult OnPostStartAddAuthor(string first_name, string last_name)
        {
            AddAuthor = true;
            SearchedFirstName = first_name;
            SearchedLastName = last_name;

            return Page();
        }

        public async Task<IActionResult> OnPostAddBook(int authorId, string newTitle, string newPublisher, DateTime newPublicationDate, bool newPaperback, int newCopiesCount)
        {
            var bookToAdd = new Book()
            {
                author_id = authorId,
                title = newTitle,
                publisher = newPublisher,
                publication_date = newPublicationDate,
                paperback = newPaperback,
                copies = newCopiesCount
            };

            var bookId = await _bookClient.Post(bookToAdd);
            bookToAdd.id = bookId;
            SelectedAuthor.books.Add(bookToAdd);
            return Page();
        }

        public async Task<IActionResult> OnPostAddBook(int authorId, string newTitle, string newPublisher, DateTime newPublicationDate, bool newPaperback, int newCopiesCount)
        {
            var bookToAdd = new Book()
            {
                author_id = authorId,
                title = newTitle,
                publisher = newPublisher,
                publication_date = newPublicationDate,
                paperback = newPaperback,
                copies = newCopiesCount
            };

            var bookId = await _bookClient.Post(bookToAdd);
            bookToAdd.id = bookId;
            SelectedAuthor.books.Add(bookToAdd);
            return Page();
        }

        public async Task<IActionResult> OnPostDeleteBook(int bookId)
        {
            var result = await _bookClient.Delete(bookId);

            return Page();
        }

        public async Task<IActionResult> OnPostDeleteAuthor(int authorId, int bookCount)
        {
            //TODO: Check if Author Has books, if so give warning that books must be removed before author.
            if (bookCount > 0)
            {
                ShowError = true;
                return Page();
            }
            
            var result = await _authorClient.Delete(authorId);

            return Page();
        }
    }
}
