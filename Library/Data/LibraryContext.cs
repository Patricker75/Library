using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

using Library.Models;
using Library.Models.Relationships;

namespace Library.Data
{
    public class LibraryContext : DbContext
    {
        public LibraryContext (DbContextOptions<LibraryContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            // Specifying ID's are Identity Types
            builder.Entity<Author>().Property(a => a.ID).UseIdentityColumn();
            builder.Entity<Book>().Property(b => b.ID).UseIdentityColumn();
            builder.Entity<Device>().Property(d => d.ID).UseIdentityColumn();
            builder.Entity<Employee>().Property(e => e.ID).UseIdentityColumn();
            builder.Entity<Member>().Property(m => m.ID).UseIdentityColumn();
            builder.Entity<Publisher>().Property(p => p.ID).UseIdentityColumn();
            builder.Entity<ResearchResource>().Property(r => r.ID).UseIdentityColumn();
            builder.Entity<Room>().Property(r => r.ID).UseIdentityColumn();
            builder.Entity<Service>().Property(s => s.ID).UseIdentityColumn();

            // Specifiying Composite PK's
            builder.Entity<Accesses>().HasKey(a => new { a.MemberID, a.ResourceID, a.TimeStamp });
            builder.Entity<Borrows>().HasKey(b => new { b.MemberID, b.DeviceID, b.ReturnDate });
            builder.Entity<CheckOuts>().HasKey(c => new { c.BookID, c.MemberID, c.ReturnDate });
            builder.Entity<Holds>().HasKey(h => new { h.BookID, h.MemberID, h.HoldDate });
            builder.Entity<Manages>().HasKey(m => new { m.EmployeeID, m.ServiceID });
            builder.Entity<Publishes>().HasKey(p => new { p.BookID, p.PublisherID});
            builder.Entity<Uses>().HasKey(u => new { u.MemberID, u.ServiceID });
            builder.Entity<Writes>().HasKey(w => new { w.AuthorID, w.BookID });

            // Enum Conversions
            builder.Entity<Book>().Property(b => b.Audience).HasConversion(v => (short)v, v => (Audience)v);
            builder.Entity<Book>().Property(b => b.Condition).HasConversion(v => (short)v, v => (Condition)v);

            builder.Entity<Device>().Property(d => d.ItemType).HasConversion(v => (short)v, v => (ItemType)v);
            builder.Entity<Device>().Property(d => d.Condition).HasConversion(v => (short)v, v => (Condition)v);

            builder.Entity<Employee>().Property(e => e.Gender).HasConversion(v => (short)v, v => (Gender)v);

            builder.Entity<Member>().Property(m => m.Gender).HasConversion(v => (short)v, v => (Gender)v);
            builder.Entity<Member>().Property(m => m.MemberType).HasConversion(v => (short)v, v => (MemberType)v);
            builder.Entity<Member>().Property(m => m.MemberStatus).HasConversion(v => (short)v, v => (MemberStatus)v);

            builder.Entity<Room>().Property(r => r.RoomType).HasConversion(v => (short)v, v => (RoomType)v);
        }

        public DbSet<Library.Models.Member> Member { get; set; } = default!;

        public DbSet<Library.Models.Room> Room { get; set; } = default!;

        public DbSet<Library.Models.Author> Author { get; set; } = default!;

        public DbSet<Library.Models.Publisher> Publisher { get; set; } = default!;

        public DbSet<Library.Models.Book> Book { get; set; } = default!;

        public DbSet<Library.Models.Service> Service { get; set; } = default!;

        public DbSet<Library.Models.Device> Device { get; set; } = default!;

        public DbSet<Library.Models.Employee> Employee { get; set; } = default!;

        public DbSet<Library.Models.Relationships.Accesses> Accesses { get; set; } = default!;

        public DbSet<Library.Models.Relationships.Borrows> Borrows { get; set; } = default!;

        public DbSet<Library.Models.Relationships.CheckOuts> CheckOuts { get; set; } = default!;

        public DbSet<Library.Models.Relationships.Holds> Holds { get; set; } = default!;

        public DbSet<Library.Models.Relationships.Manages> Manages { get; set; } = default!;

        public DbSet<Library.Models.Relationships.Publishes> Publishes { get; set; } = default!;

        public DbSet<Library.Models.Relationships.Uses> Uses { get; set; } = default!;

        public DbSet<Library.Models.Relationships.Writes> Writes { get; set; } = default!;
    }
}
