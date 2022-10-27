using Library.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Library.Pages.Books
{
    public class ViewBookModel : PageModel
    {
        public Book Book { get; set; }

        public void OnGetByObject(Book book)
        {
            Book = book;
        }
    }
}
