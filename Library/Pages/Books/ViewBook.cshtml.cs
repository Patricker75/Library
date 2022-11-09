using Library.Data;
using Library.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Library.Pages.Books
{
    public class ViewBookModel : PageModel
    {
        public List<Book> Books { get; set; } = default!;

        [BindProperty]
        public int BookID { get; set; } = default!;

        [BindProperty]
        public string Message { get; set; } = string.Empty;

        private readonly LibraryContext _context;

        public ViewBookModel(LibraryContext context)
        {
            _context = context;
        }

        public IActionResult OnGet(int? id, string? message)
        {
            if (id == null)
            {
                return NotFound();
            }

            if (message != null)
            {
                Message = message;
            }

            Book? book = _context.Book.Find(id);

            if (book == null)
            {
                return NotFound();
            }

            Books = _context.Book.Where(b => b.Title == book.Title).ToList();

            BookID = (int)id;

            return Page();
        }

        private int CalculateCheckOut(int memberID)
        {
            if (_context.CheckOuts == null)
            {
                return -1;
            }
            
            return _context.CheckOuts.Where(c => c.MemberID == memberID && !c.Returned).Count();
        }

        private int? SearchBook(int id)
        {
            Book? b = _context.Book.Find(id);
            if (b == null)
            {
                return null;
            }

            List<Book> copies = _context.Book.Where(_b => _b.Title == b.Title).ToList();
            foreach (Book book in copies)
            {
                // Check if book is checked out
                if (_context.CheckOuts.Where(c => c.BookID == book.ID && !c.Returned) != null) {
                    copies.Remove(book);
                }
            }

            if (copies.Count > 0)
            {
                return copies.First().ID;
            }

            return null;
        }

        public IActionResult OnPost()
        {
            int? memberID = HttpContext.Session.GetInt32("loginID");

            if (memberID == null)
            {
                return RedirectToPage("../Error");
            }
            
            Member? member = _context.Member.Find(memberID);

            if (member == null)
            {
                return RedirectToPage("../Error");
            }

            int booksOut = CalculateCheckOut(member.ID);
            
            if (booksOut == -1)
            {
                return RedirectToPage("../Error");
            }

            // Checks if Member can check out book
            switch (member.MemberType)
            {
                case MemberType.Student:
                    if (booksOut + 1 > 5)
                    {
                        return RedirectToPage("../Error");
                    }
                    break;
                case MemberType.Professional:
                    if (booksOut + 1 > 10)
                    {
                        return RedirectToPage("../Error");
                    }
                    break;
            }

            int? checkOutID = SearchBook(BookID);

            if (checkOutID == null)
            {
                //HOLD BOOK
            }
            else
            {
                // CHECKOUT BOOK
            }

            return RedirectToPage("ViewBook", new { id = BookID, message = "Check Out Successful" });
        }
    }
}
