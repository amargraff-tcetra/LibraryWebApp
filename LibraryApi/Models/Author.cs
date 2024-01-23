namespace LibraryApi.Models
{
    public class Author
    {
        //[Column(name: "id")]
        //public int Id { get; set; }
        //[Column(name: "first_name")]
        //public string FirstName { get; set; } = string.Empty;
        //[Column(name:"last_name")]
        //public string LastName { get; set; } = string.Empty;
        //[Column(name: "date_of_birth")]
        //public DateTime DateOfBirth { get; set; }

        public int id { get; set; }
        public string first_name { get; set; } = string.Empty;
        public string last_name { get; set; } = string.Empty;
        public DateTime date_of_birth { get; set; }

        public Author() { }
    }
}