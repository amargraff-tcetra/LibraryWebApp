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
        public List<Book> Books { get; set; } = new List<Book>();
        public Book? SelectedBook { get; set; }

        public IndexModel(ILogger<IndexModel> logger, IBookClient bookClient, IConfiguration configuration)
        {
            _logger = logger;
            _bookClient = bookClient;
        }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostBooks(string? bookTitleKey)
        {
            SelectedBook = null;
            Books = await _bookClient.Get(bookTitleKey?.Trim() ?? string.Empty);
            return Page();
        }

        public async Task<IActionResult> OnPostBooksByAuthor(string? authorNameKey)
        {
            SelectedBook = null;
            Books = await _bookClient.Get(string.Empty);
            authorNameKey = authorNameKey?.Trim() ?? string.Empty;
            Books = Books.Where(b => b.author.first_name.Contains(authorNameKey, StringComparison.InvariantCultureIgnoreCase) || b.author.last_name.Contains(authorNameKey, StringComparison.InvariantCultureIgnoreCase)).ToList();
            return Page();
        }        
        
        public async Task<IActionResult> OnPostBooksByPublisher(string? publisherNameKey)
        {
            SelectedBook = null;
            Books = await _bookClient.Get(string.Empty);
            publisherNameKey = publisherNameKey?.Trim() ?? string.Empty;
            Books = Books.Where(b => b.publisher.Contains(publisherNameKey, StringComparison.InvariantCultureIgnoreCase)).ToList();
            return Page();
        }
    }
}
