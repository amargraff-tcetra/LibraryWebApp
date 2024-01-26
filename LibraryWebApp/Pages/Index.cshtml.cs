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

            Books = await _bookClient.Get(SearchedTitle, SearchedAuthor, SearchedPublisher);

            return Page();
        }
    }
}
