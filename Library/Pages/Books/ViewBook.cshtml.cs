using Library.Data;
using Library.Models;
using Library.Models.Relationships;
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

            // Check if member has a copy of book checked out
            int? memberID = HttpContext.Session.GetInt32("loginID");
            foreach (Book book in copies)
            {
                if (_context.CheckOuts.Where(c => c.MemberID == memberID && c.BookID == book.ID && !c.Returned).Any()) {
                    return -1;
                }
                if (_context.Holds.Where(h => h.MemberID == memberID && h.BookTitle == b.Title && h.Waiting).Any())
                {
                    return -1;
                }
            }

            b = null;
            foreach (Book book in copies)
            {
                
                // Check record if a book hasnt been checked out ever
                if (!_context.CheckOuts.Where(_b => _b.BookID == book.ID).Any())
                {
                    b = book;
                    break;
                }
                // Check if book is checked out, see if its returned
                else if (!_context.CheckOuts.Where(_b => _b.BookID == book.ID && !_b.Returned).Any())
                {
                    b = book;
                    break;
                }
            }

            if (b != null)
            {
                return b.ID;
            }

            return null;
        }

        private void CheckOutBook(int memberID, int bookID)
        {
            _context.CheckOuts.Add(new Models.Relationships.CheckOuts()
            {
                BookID = bookID,
                MemberID = memberID,
                Returned = false,
                ReturnDate = DateTime.Now.AddDays(14)
            });
            _context.SaveChanges();
        }

        private void HoldBook(int memberID, int bookID)
        {
            Book? b = _context.Book.Find(bookID);
            int? authorID = _context.Writes.Where(w => w.BookID == bookID).First().AuthorID;

            if (authorID == null || b == null)
            {
                return;
            }

            _context.Holds.Add(new Holds()
            {
                BookTitle = b.Title,
                MemberID = memberID,
                AuthorID = (int)authorID,
                HoldDate = DateTime.Today,
                Waiting = true
            });

            _context.SaveChanges();
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

            int? availableBookID = SearchBook(BookID);

            if (availableBookID == -1)
            {
                return RedirectToPage("ViewBook", new { id = BookID, message = "Book Already Checked Out/Held" });
            }
            else if (availableBookID != null)
            {
                CheckOutBook((int)memberID, (int)availableBookID);
                return RedirectToPage("ViewBook", new { id = BookID, message = "Check Out Successful" });
            }
            else
            {
                HoldBook((int)memberID, BookID);
                return RedirectToPage("ViewBook", new { id = BookID, message = "Holding Book" });
            }
        }
    }
}
