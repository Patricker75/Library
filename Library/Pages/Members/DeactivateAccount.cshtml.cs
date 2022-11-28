using Library.Data;
using Library.Models;
using Library.Models.Relationships;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Library.Pages.Members
{
    [BindProperties]
    public class DeactivateAccountModel : PageModel
    {
        public int MemberID { get; set; }

        public string Message { get; set; } = string.Empty;
        public bool Confirmation { get; set; } = false;

        private readonly LibraryContext _context;
        
        public DeactivateAccountModel(LibraryContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            string? type = HttpContext.Session.GetString("loginType");
            int? id = HttpContext.Session.GetInt32("loginID");
            if (type != "member")
            {
                return RedirectToPage("/Index");
            }

            Message = "Are You Sure You Want to Deactivate Your Account?";
            Confirmation = false;
            MemberID = (int)id;

            return Page();
        }

        public IActionResult OnPost()
        {
            Message = "Are You Really Sure?";
            Confirmation = true;

            return Page();
        }

        public IActionResult OnPostDeactivate()
        {
            Member? m = _context.Members.FirstOrDefault(m => m.ID == MemberID);
            if (m == null)
            {
                return Page();
            }

            m.Status = MemberStatus.Inactive;

            // Returns all books, devices, and room
            IList<CheckOut> memberCO = _context.CheckOuts.Where(co => co.MemberID == m.ID && !co.IsReturned).ToList();
            foreach (CheckOut checkOut in memberCO)
            {
                checkOut.ReturnDate = DateTime.Now;
                checkOut.IsReturned = true;
            }

            Room? room = _context.Rooms.FirstOrDefault(r => r.MemberID == m.ID);
            if (room != null)
            {
                room.MemberID = null;
                _context.Attach(room).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            }

            _context.AttachRange(memberCO);
            _context.Attach(m).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();

            return RedirectToPage("/Logout");
        }

        public IActionResult OnPostAbort()
        {
            return RedirectToPage("/Members/Profile");
        }
    }
}
