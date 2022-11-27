using Library.Data;
using Library.Models;
using Library.Models.Relationships;
using Library.Models.Views;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Library.Pages.Employees
{
    public class ProfileModel : PageModel
    {
        public Employee Employee { get; set; } = default!;

        public IList<UnpaidFine> UnpaidFines { get; set; } = default!;
        public IList<Room> ReservedRooms { get; set; } = default!;
        public IList<Room> UnavailableRooms { get; set; } = default!;
        public IList<Service> UnavailableServices { get; set; } = default!;

        private readonly LibraryContext _context;
        
        public ProfileModel(LibraryContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            if (HttpContext.Session.GetString("loginType") != "employee")
            {
                if (HttpContext.Session.GetString("loginType") == "member")
                {
                    return RedirectToPage("/Members/Profile");
                }
                else
                {
                    return RedirectToPage("/Login");
                }
            }

            int? employeeID = HttpContext.Session.GetInt32("loginID");
            if (employeeID == null)
            {
                return RedirectToPage("../Error");
            }

            Employee = _context.Employees.Where(e => e.ID == employeeID).First();

            return Page();
        }

        public Employee? GetSupervisor(int? supervisorID)
        {
            return _context.Employees.FirstOrDefault(e => e.ID == supervisorID);
        }

        public void GetUnpaidFines()
        {
            if (_context.UnpaidFines != null)
            {
                UnpaidFines = _context.UnpaidFines.ToList();
            }
        }

        public void GetReservedRooms()
        {
            if (_context.Rooms != null)
            {
                ReservedRooms = _context.Rooms.Where(r => r.MemberID != null).ToList();
            }
        }

        public Member? GetMember(int? memberID)
        {
            return _context.Members.FirstOrDefault(m => m.ID == memberID);
        }

        public void GetUnavailableRooms()
        {
            if (_context.Rooms != null)
            {
                UnavailableRooms = _context.Rooms.Where(r => !r.IsAvailable).ToList();
            }
        }

        public void GetUnavailableServices()
        {
            if (_context.Services != null)
            {
                UnavailableServices = _context.Services.Where(s => !s.Availability).ToList();
            }
        }

        public string GetItemType(int checkoutID)
        {
            CheckOut? co = _context.CheckOuts.FirstOrDefault(co => co.ID == checkoutID);
            if (co == null)
            {
                return "";
            }

            return co.Type.ToString();
        }

        public string GetItem(int checkoutID)
        {
            CheckOut? co = _context.CheckOuts.FirstOrDefault(co => co.ID == checkoutID);
            if (co == null)
            {
                return "";
            }

            switch (co.Type)
            {
                case ItemType.Book:
                    return _context.Books.First(b => b.ID == co.ItemID).Title;
                case ItemType.Device:
                    return _context.Devices.First(d => d.ID == co.ItemID).Name;
                default:
                    return "";
            }
        }
    }
}
