using Library.Data;
using Library.Models.Views;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace Library.Pages.Reports
{
    public class CheckoutReportModel : PageModel
    {
        [BindProperty]
        [DataType(DataType.Date)]
        public DateTime Start { get; set; }

        [BindProperty]
        [DataType(DataType.Date)]
        public DateTime End { get; set; }

        public bool GenerateReport = false;

        public IList<CheckoutReport> Reports { get; set; } = default!;

        private readonly LibraryContext _context;

        public CheckoutReportModel(LibraryContext context)
        {
            _context = context;
        }

        public void OnGet()
        {
            Start = DateTime.Today.AddDays(-30);
            End = DateTime.Today;
        }
        public IActionResult OnPost()
        {
            End = End.AddDays(1).AddSeconds(-1);
            if (!ModelState.IsValid)
            {
                return Page();
            }

            Reports = _context.CheckoutReports.Where(vr => Start <= vr.CheckoutDate && vr.CheckoutDate <= End).ToList();
            GenerateReport = true;
            return Page();
        }
    }
}
