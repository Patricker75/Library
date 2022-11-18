using Library.Data;
using Library.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Library.Pages.Reports
{
    public class BookStat
    {
        public Book Book { get; set; } = default!;
        public int Count { get; set; }
        public Author Author { get; set; } = default!;
    }

    public class ItemCatalogModel : PageModel
    {
        public IList<BookStat> Books { get; set; } = default!;
        public IList<Device> Devices { get; set; } = default!;
        public IList<Room> Rooms { get; set; } = default!;
        public IList<Service> Services { get; set; } = default!;

        private readonly LibraryContext _context;

        public ItemCatalogModel(LibraryContext context)
        {
            _context = context;
        }

        public void OnGet(bool book, bool device, bool room, bool service)
        {
            if (book && _context.Book != null)
            {
                var query = _context.Book.GroupBy(b => b.Title);
                
                foreach (var title in query)
                {
                    //Books.Add(new BookStat()
                    //{
                    //    Book = query.First(),
                    //    Count = query.Count()
                    //});
                }
            }

            if (device && _context.Device != null)
            {
                Devices = _context.Device.ToList();
            }

            if (room && _context.Room != null)
            {
                Rooms = _context.Room.ToList();
            }

            if (service && _context.Service != null)
            {
                Services = _context.Service.ToList();
            }
        }
    }
}
