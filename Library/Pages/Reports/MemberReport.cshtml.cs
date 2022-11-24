using Library.Data;
using Library.Models.Relationships;
using Library.Models.Views;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
namespace Library.Pages.Reports
{
    public class MemberReportModel : PageModel
    {
        public IList<MemberReport> Reports { get; set; } = default!;

        private LibraryContext _context;
        
        public MemberReportModel(LibraryContext context)
        {
            _context = context;
        }   
        
        public IActionResult OnGet()
        {
            if (_context.MemberReports != null)
            {
                Reports = _context.MemberReports.ToList();
            }

            return Page();
        }

    }
}
