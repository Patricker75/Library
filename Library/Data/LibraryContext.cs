using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Library.Models;
using Library.Models.Relationships;
using Library.Models.Views;

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
            base.OnModelCreating(builder);

            // Specifying ID's as Identity Columns
            builder.Entity<Author>().Property(a => a.ID).UseIdentityColumn();
            builder.Entity<Book>().Property(b => b.ID).UseIdentityColumn();
            builder.Entity<Device>().Property(d => d.ID).UseIdentityColumn();
            builder.Entity<Employee>().Property(e => e.ID).UseIdentityColumn();
            builder.Entity<Member>().Property(m => m.ID).UseIdentityColumn();
            builder.Entity<Notification>().Property(n => n.ID).UseIdentityColumn();
            builder.Entity<Publisher>().Property(p => p.ID).UseIdentityColumn();
            builder.Entity<Resource>().Property(r => r.ID).UseIdentityColumn();
            builder.Entity<Service>().Property(s => s.ID).UseIdentityColumn();
            builder.Entity<Report>().Property(v => v.ID).UseIdentityColumn();

            builder.Entity<CheckOut>().Property(co => co.ID).UseIdentityColumn();
            builder.Entity<Fine>().Property(f => f.ID).UseIdentityColumn();
            builder.Entity<Hold>().Property(h => h.ID).UseIdentityColumn();

            builder.Entity<Room>().HasKey(r => r.Location);

            // Specifying Composite Primary Keys
            builder.Entity<Access>().HasKey(a => new { a.TimeStamp, a.MemberID, a.ResourceID });
            builder.Entity<Manage>().HasKey(m => new { m.EmployeeID, m.ServiceID });
            builder.Entity<Use>().HasKey(u => new { u.TimeStamp, u.MemberID, u.ServiceID });

            // Specifiying Enum Conversions
            builder.Entity<Book>().Property(b => b.Audience).HasConversion(v => (short)v, v => (Audience)v);
            builder.Entity<Book>().Property(b => b.Genre).HasConversion(v => (short)v, v => (Genre)v);
            builder.Entity<Device>().Property(d => d.Type).HasConversion(v => (short)v, v => (DeviceType)v);
            builder.Entity<Employee>().Property(e => e.Gender).HasConversion(v => (short)v, v => (Gender)v);
            builder.Entity<Employee>().Property(e => e.JobRole).HasConversion(v => (short)v, v => (JobRole)v);
            builder.Entity<Member>().Property(m => m.Gender).HasConversion(v => (short)v, v => (Gender)v);
            builder.Entity<Member>().Property(m => m.Status).HasConversion(v => (short)v, v => (MemberStatus)v);
            builder.Entity<Member>().Property(m => m.Type).HasConversion(v => (short)v, v => (MemberType)v);
            builder.Entity<Room>().Property(r => r.Type).HasConversion(v => (short)v, v => (RoomType)v);

            builder.Entity<CheckOut>().Property(co => co.Type).HasConversion(v => (short)v, v => (ItemType)v);
            builder.Entity<Fine>().Property(f => f.Type).HasConversion(v => (short)v, v => (ItemType)v);

            HandleViews(builder);
        }

        private void HandleViews(ModelBuilder builder)
        {
            //builder.Entity<MemberReport>().ToView("member_report").HasNoKey();
            builder.Entity<MemberReport>().ToView("memberreport").HasNoKey();
            builder.Entity<CheckoutReport>().ToView("check_out_report").HasNoKey();
            builder.Entity<MemberReport>().Property(vr => vr.ID).UseIdentityColumn();
            builder.Entity<CheckoutReport>().Property(co => co.ItemType).HasConversion(v => (short)v, v => (ItemType)v);
        }

        public DbSet<Author> Authors { get; set; } = default!;
        public DbSet<Book> Books { get; set; } = default!;
        public DbSet<Device> Devices { get; set; } = default!;
        public DbSet<Employee> Employees { get; set; } = default!;
        public DbSet<LoginUser> Logins { get; set; } = default!;
        public DbSet<Member> Members { get; set; } = default!;
        public DbSet<Notification> Notifications { get; set; } = default!;
        public DbSet<Publisher> Publishers { get; set; } = default!;
        public DbSet<Resource> Resources { get; set; } = default!;
        public DbSet<Room> Rooms { get; set; } = default!;
        public DbSet<Service> Services { get; set; } = default!;


        public DbSet<Access> Accesses { get; set; } = default!;
        public DbSet<CheckOut> CheckOuts { get; set; } = default!;
        public DbSet<Fine> Fines { get; set; } = default!;
        public DbSet<Hold> Holds { get; set; } = default!;
        public DbSet<Manage> Manages { get; set; } = default!;
        public DbSet<Use> Uses { get; set; } = default!;


        public DbSet<MemberReport> MemberReports { get; set; } = default!;
        public DbSet<CheckoutReport> CheckoutReports { get; set; } = default!;
    }
}
