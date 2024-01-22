namespace LibraryApi.Models
{
    public class Author
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public DateTime DateOfBirth { get; set; }
        public string CountryOfOrigin { get; set; } = string.Empty;

        public Author()
        {
            
        }

    }
}