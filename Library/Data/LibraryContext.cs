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

        public DbSet<Library.Models.Member> Member { get; set; } = default!;

        public DbSet<Library.Models.Room> Room { get; set; } = default!;

        public DbSet<Library.Models.Author> Author { get; set; } = default!;

        public DbSet<Library.Models.Publisher> Publisher { get; set; } = default!;

        public DbSet<Library.Models.Book> Book { get; set; } = default!;

        public DbSet<Library.Models.Service> Service { get; set; } = default!;

        public DbSet<Library.Models.Device> Device { get; set; } = default!;

        public DbSet<Library.Models.Employee> Employee { get; set; }

        public DbSet<Library.Models.Notification> Notification { get; set; }
    }
}
