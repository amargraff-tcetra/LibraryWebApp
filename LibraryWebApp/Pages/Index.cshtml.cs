using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LibraryWebApp.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        public HttpClient httpClient { get; set; }
        public List<string>? BookTitles { get; set; } = new List<string>();

        public IndexModel(ILogger<IndexModel> logger, HttpClient httpClient)
        {
            _logger = logger;
            this.httpClient = httpClient;
        }

        public void OnGet()
        {

        }

        public async Task<IActionResult> OnPostBooks(string bookTitleKey)
        {
            if (string.IsNullOrWhiteSpace(bookTitleKey)) {
                BookTitles = await httpClient.GetFromJsonAsync<List<string>>("http://library-api:8080/api/books");
            }
            else
            {
                BookTitles = await httpClient.GetFromJsonAsync<List<string>>($"http://library-api:8080/api/books/{bookTitleKey.Trim()}");
            }
            

            return Page();
        }
    }
}
