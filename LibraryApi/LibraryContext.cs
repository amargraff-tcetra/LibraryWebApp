using LibraryApi.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryApi
{
    public class LibraryContext : DbContext
    {
        public LibraryContext(DbContextOptions<LibraryContext> options): base(options) { }

        public virtual DbSet<Book> Books { get; set; }
        public virtual DbSet<Author> Authors { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Book>()
                .HasOne(b => b.author)
                .WithMany(a => a.books)
                .HasForeignKey(b => b.author_id);

            modelBuilder.Entity<Author>()
                .HasMany(a => a.books)
                .WithOne(b => b.author)
                .HasForeignKey(b => b.author_id);
        }
    }
}
