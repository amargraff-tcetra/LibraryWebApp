namespace LibraryApi.Models
{
    public class Book
    {
        public int id { get; set; }
        public int author_id { get; set; }
        public string title { get; set; } = string.Empty;
        public string publisher { get; set; } = string.Empty;
        public DateTime publication_date { get; set; }
        public bool paperback { get; set; } = false;        
        public int copies { get; set; }
    }
}
