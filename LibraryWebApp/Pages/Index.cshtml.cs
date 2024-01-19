using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LibraryWebApp.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        public HttpClient httpClient { get; set; }
        public List<string>? bookTitles { get; set; } = new List<string>();

        public IndexModel(ILogger<IndexModel> logger, HttpClient httpClient)
        {
            _logger = logger;
            this.httpClient = httpClient;
        }

        public void OnGet()
        {

        }

        public async Task<IActionResult> OnPostBooks()
        {
            //bookTitles = await httpClient.GetFromJsonAsync<List<string>>("http://localhost:5091/api/books");
            bookTitles = await httpClient.GetFromJsonAsync<List<string>>("http://library_api:5091/api/books");
            return Page();
        }
    }
}
