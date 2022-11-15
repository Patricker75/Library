using Library.Data;
using Library.Models;
using Library.Models.Relationships;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Library.Pages.Reports
{
    [BindProperties]
    public class ReportCOHistoryModel : PageModel
    {

        public IList<Book> Books { get; set; } = default!;
        public IList<Device> Devices { get; set; } = default!;
        public IList<Service> Services { get; set; } = default!;

        private readonly LibraryContext _context;

        public ReportCOHistoryModel(LibraryContext context) 
        { 
            _context = context;
        }

        public void OnGet(int memberID, bool fetchBooks, bool fetchDevices, bool fetchRooms, bool fetchServices, DateTime start, DateTime end)
        {
            if (fetchBooks && _context.Book != null)
            {
                IList<CheckOuts> coList = _context.CheckOuts.Where(co => co.ReturnDate >= start && co.ReturnDate <= end && co.MemberID == memberID).ToList();

                Books = _context.Book.Where(b => coList.Where(co => co.BookID == b.ID).Any()).ToList();
            }

            if (fetchDevices && _context.Device != null)
            {
                IList<Borrows> bList = _context.Borrows.Where(b => b.ReturnDate >= start && b.ReturnDate <= end && b.MemberID == memberID).ToList();

                Devices = _context.Device.Where(d => bList.Where(b => b.DeviceID == d.ID).Any()).ToList();
            }

            if (fetchServices && _context.Device != null)
            {
                IList<Uses> uList = _context.Uses.Where(u => u.TimeStamp >= start && u.TimeStamp <= end && u.MemberID == memberID).ToList();

                Services = _context.Service.Where(s => uList.Where(u => u.ServiceID == u.ServiceID).Any()).ToList();
            }
        }
    }
}
