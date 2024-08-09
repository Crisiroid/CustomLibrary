using CustomLibrary.Models;
using Microsoft.EntityFrameworkCore;

namespace CustomLibrary.Data
{
    public class LibraryDBContext : DbContext
    {
        public LibraryDBContext(DbContextOptions options): base(options) { }
        public DbSet<User> Users { get; set; }
    }
}
