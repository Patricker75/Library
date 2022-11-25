using Library.Data;
using Library.Models.Views;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Library.Pages.Reports
{
    public class CheckoutReportModel : PageModel
    {
        public IList<CheckoutReport> Reports { get; set; } = default!;

        private readonly LibraryContext _context;

        public CheckoutReportModel(LibraryContext context)
        {
            _context = context;
        }

        public void OnGet()
        {
            if (_context.CheckoutReports != null)
            {
                Reports = _context.CheckoutReports.ToList();
            }
        }
    }
}
