using Library.Data;
using Library.Models;
using Library.Models.Relationships;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Library.Pages.Books
{
    public class ViewBookModel : PageModel
    {
        [BindProperty]
        public Book Book { get; set; } = default!;

        [BindProperty]
        public Author Author { get; set; } = default!;

        [BindProperty]
        public Publisher Publisher { get; set; } = default!;

        public string Message { get; set; } = string.Empty;

        private readonly LibraryContext _context;

        public ViewBookModel(LibraryContext context)
        {
            _context = context;
        }

        public IActionResult OnGet(int? bookID, string message)
        {
            Message = message;

            if (bookID == null)
            {
                return RedirectToPage("/Books/Index");
            }

            Book? b = _context.Books.FirstOrDefault(b => b.ID == bookID);
            if (b == null)
            {
                return RedirectToPage("/Books/Index");
            }

            Book = b;

            Author? a = _context.Authors.FirstOrDefault(a => a.ID == b.AuthorID);
            if (a == null)
            {
                return RedirectToPage("/Books/Index");
            }

            Publisher? p = _context.Publishers.FirstOrDefault(p => p.ID == b.PublisherID);
            if (p == null)
            {
                return RedirectToPage("/Books/Index");
            }

            Author = a;
            Publisher = p;

            return Page();
        }
        
        private void CheckOutBook(Book book, Member member)
        {
            // Days after for due date
            int interval = 0;
            switch (member.Type)
            {
                case MemberType.Student:
                    interval = 14;
                    break;
                case MemberType.Professional:
                    interval = 28;
                    break;
            }

            _context.Database.ExecuteSqlRaw("INSERT INTO check_out (check_out_date, due_date, item_type, item_id, member_id, returned)\r\nVALUES ({0},{1},{2},{3},{4},{5})",
                DateTime.Now, DateTime.Now.AddDays(interval), ItemType.Book, Book.ID, member.ID, 0);

            _context.SaveChanges();
        }

        private void HoldBook(Book book, Member member)
        {
            // Check if member has a current hold on the book
            if (_context.Holds.Where(h => h.IsWaiting && h.MemberID == member.ID && h.BookID == book.ID).Any())
            {
                return;
            }

            _context.Holds.Add(new Hold()
            {
                BookID = book.ID,
                MemberID = member.ID,
                HoldDate = DateTime.Now,
                IsWaiting = true
            });

            _context.SaveChanges();
        }

        public IActionResult OnPostCheckOut()
        {
            Book? b = _context.Books.FirstOrDefault(b => b.ID == Book.ID);
            if (b == null)
            {
                return Page();
            }

            Book = b;

            int? id = HttpContext.Session.GetInt32("loginID");
            if (id == null)
            {
                return Page();
            }
            
            Member? m = _context.Members.FirstOrDefault(m => m.ID == id);
            if (m == null)
            {
                return Page();
            }

            if (m.Status != MemberStatus.Active)
            {
                return Page();
            }

            // Check if member will exceed their checkout limit
            if (_context.CheckOuts.Where(co => co.MemberID == m.ID && !co.IsReturned).Count() + 1 > m.CheckOutLimit) 
            {
                return Page();
            }

            // Check if member has the book already checked out
            if (_context.CheckOuts.Where(co => co.MemberID == m.ID && co.ItemID == Book.ID && co.Type == ItemType.Book && !co.IsReturned).Any())
            {
                return Page();
            }

            if (b.Quantity == 0)
            {
                HoldBook(b, m);

                return RedirectToAction("Get", new { bookID = b.ID, message = "Hold Request Sucessful" });
            }
            else
            {
                CheckOutBook(b, m);

                return RedirectToAction("Get", new { bookID = b.ID, message = "Checked Out Book" });
            }
           
        }

        public IActionResult OnPostEdit()
        {
            return RedirectToPage("/Books/Edit", new { bookID = Book.ID });
        }
    }
}
