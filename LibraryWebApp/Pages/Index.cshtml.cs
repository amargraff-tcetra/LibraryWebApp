using LibraryWebApp.Abstraction;
using LibraryWebApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LibraryWebApp.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IBookClient _bookClient;
        private readonly IAuthorClient _authorClient;
        public string SearchedTitle { get; set; } = string.Empty;
        public string SearchedAuthor { get; set; } = string.Empty;
        public string SearchedPublisher { get; set; } = string.Empty;
        public List<Book> Books { get; set; } = new List<Book>();
        public Book? SelectedBook { get; set; }

        public IndexModel(ILogger<IndexModel> logger, IBookClient bookClient, IAuthorClient authorClient, IConfiguration configuration)
        {
            _logger = logger;
            _bookClient = bookClient;
            _authorClient = authorClient;
        }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostBooks(string? bookTitleKey, string? authorNameKey, string? publisherNameKey)
        {
            SearchedTitle = bookTitleKey?.Trim() ?? string.Empty;
            SearchedAuthor = authorNameKey?.Trim() ?? string.Empty;
            SearchedPublisher = publisherNameKey?.Trim() ?? string.Empty;

            if (string.IsNullOrWhiteSpace(SearchedTitle) && string.IsNullOrWhiteSpace(SearchedAuthor) && string.IsNullOrWhiteSpace(SearchedPublisher))
            {
                Books = await _bookClient.Get(SearchedTitle);
                return Page();
            }

            var booksByTitle = new List<Book>();
            if (!string.IsNullOrWhiteSpace(SearchedTitle))
            {
                booksByTitle = await _bookClient.Get(SearchedTitle);
            }

            var booksByAuthor = new List<Book>();
            if (!string.IsNullOrWhiteSpace(SearchedAuthor))
            {
                var authors = await _authorClient.Get(SearchedAuthor) ?? new List<Author>();
                booksByAuthor = authors.SelectMany(a => a.books).ToList();
                booksByAuthor.ForEach(b => b.author = authors.Where(a => a.id == b.author_id).FirstOrDefault() ?? new Author());
            }

            var booksByPublisher = new List<Book>();
            if (!string.IsNullOrWhiteSpace(SearchedPublisher))
            {
                booksByPublisher = await _bookClient.Get(string.Empty);
                booksByPublisher = booksByPublisher.Where(b => b.publisher.Contains(SearchedPublisher, StringComparison.InvariantCultureIgnoreCase)).ToList();
            }

            booksByAuthor = booksByAuthor.Where(b => !booksByTitle.Select(x => x.id).Contains(b.id)).ToList();
            booksByPublisher = booksByPublisher.Where(b => !booksByTitle.Select(x => x.id).Contains(b.id)).ToList();
            booksByPublisher = booksByPublisher.Where(b => !booksByAuthor.Select(x => x.id).Contains(b.id)).ToList();

            Books.AddRange(booksByTitle.Concat(booksByAuthor).Concat(booksByPublisher));
            Books = Books.Distinct().ToList();

            return Page();
        }
    }
}
