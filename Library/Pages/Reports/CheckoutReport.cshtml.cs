using Library.Data;
using Library.Models.Views;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace Library.Pages.Reports
{
    public enum CheckoutType
    {
        Book = 0,
        Device = 1,
        Both = 2
    }

    public class CheckoutReportModel : PageModel
    {
        [BindProperty]
        [DataType(DataType.Date)]
        public DateTime Start { get; set; }

        [BindProperty]
        [DataType(DataType.Date)]
        public DateTime End { get; set; }

        [BindProperty]
        public CheckoutType FilterType { get; set; }

        public bool GenerateReport = false;

        public IList<CheckoutReport> Reports { get; set; } = default!;

        private readonly LibraryContext _context;

        public CheckoutReportModel(LibraryContext context)
        {
            _context = context;
            FilterType = CheckoutType.Both;
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

            if (FilterType == CheckoutType.Both) 
            {
                Reports = _context.CheckoutReports.Where(vr => Start <= vr.CheckoutDate && vr.CheckoutDate <= End).ToList();
            }
            else
            {
                Reports = _context.CheckoutReports.Where(vr => Start <= vr.CheckoutDate && vr.CheckoutDate <= End && vr.ItemType == (ItemType)(int)FilterType).ToList();
            }

            GenerateReport = true;

            return Page();
        }
    }
}
