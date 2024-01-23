using LibraryWebApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LibraryWebApp.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        public HttpClient httpClient { get; set; }
        public List<Book> Books { get; set; } = new List<Book>();
        public Book? SelectedBook { get; set; }

        public IndexModel(ILogger<IndexModel> logger, HttpClient httpClient, IConfiguration configuration)
        {
            _logger = logger;
            this.httpClient = httpClient;
            var libraryHostBaseUri = configuration.GetSection("LIBRARY_API_HOST").Value ?? string.Empty;
            this.httpClient.BaseAddress = new Uri(libraryHostBaseUri);
        }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostBooks(string? bookTitleKey)
        {
            SelectedBook = null;
            Books = await httpClient.GetFromJsonAsync<List<Book>>($"api/books?key={bookTitleKey?.Trim() ?? string.Empty}") ?? new List<Book>();
            return Page();
        }

        public async Task<IActionResult> OnPostBooksByAuthor(string? authorNameKey)
        {
            SelectedBook = null;
            Books = await httpClient.GetFromJsonAsync<List<Book>>($"api/books?key=") ?? new List<Book>();
            authorNameKey = authorNameKey?.Trim() ?? string.Empty;
            Books = Books.Where(b => b.author.first_name.Contains(authorNameKey, StringComparison.InvariantCultureIgnoreCase) || b.author.last_name.Contains(authorNameKey, StringComparison.InvariantCultureIgnoreCase)).ToList();
            return Page();
        }        
        
        public async Task<IActionResult> OnPostBooksByPublisher(string? publisherNameKey)
        {
            SelectedBook = null;
            Books = await httpClient.GetFromJsonAsync<List<Book>>($"api/books?key=") ?? new List<Book>();
            publisherNameKey = publisherNameKey?.Trim() ?? string.Empty;
            Books = Books.Where(b => b.publisher.Contains(publisherNameKey, StringComparison.InvariantCultureIgnoreCase)).ToList();
            return Page();
        }
    }
}
