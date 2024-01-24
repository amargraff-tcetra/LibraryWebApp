using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryApi.Models
{
    [Table(name: "book")]
    public class Book
    {
        //[Column(name:"id")]
        //public int Id { get; set; }
        //[Column(name: "author_id")]
        //public int AuthorId { get; set; }
        //[Column(name: "title")]
        //public string Title { get; set; } = string.Empty;
        //[Column(name: "Publisher")]
        //public string Publisher { get; set; } = string.Empty;
        //[Column(name: "publication_date")]
        //public DateTime PublicationDate { get; set; }
        //[Column(name: "paperback")]
        //public bool Paperback { get; set; } = false;
        //[Column(name: "copies")]
        //public int Copies { get; set; }
        //public Author Author { get; set; } = new Author();


        public int id { get; set; }
        public int author_id { get; set; }
        [StringLength(200)]
        public string title { get; set; } = string.Empty;
        [StringLength(200)]
        public string publisher { get; set; } = string.Empty;
        public DateTime publication_date { get; set; }
        public bool paperback { get; set; } = false;
        public int copies { get; set; }
        public Author author { get; set; } = new Author();
    }
}
