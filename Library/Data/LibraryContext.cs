using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Library.Models;

namespace Library.Data
{
    public class LibraryContext : DbContext
    {
        public LibraryContext (DbContextOptions<LibraryContext> options)
            : base(options)
        {
        }

        public DbSet<Library.Models.Room> Rooms { get; set; } = default!;

        public DbSet<Library.Models.Author> Author { get; set; }

        public DbSet<Library.Models.Publisher> Publisher { get; set; }

        public DbSet<Library.Models.Book> Book { get; set; }

        public DbSet<Library.Models.Service> Service { get; set; }

        public DbSet<Library.Models.Device> Device { get; set; }

        public DbSet<Library.Models.Research_Resource> Research_Resource { get; set; }
    }
}
