using Library.Data;
using Library.Models.Views;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace Library.Pages.Reports
{
    public class FeeReportModel : PageModel
    {
        [BindProperty]
        [DataType(DataType.Date)]
        public DateTime Start { get; set; }

        [BindProperty]
        [DataType(DataType.Date)]
        public DateTime End { get; set; }

        public bool GenerateReport = false;

        public IOrderedEnumerable<FeeReport> Unpaid { get; set; } = default!;

        private LibraryContext _context;

        public FeeReportModel(LibraryContext context)
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

            Unpaid = from fine in _context.FeeReports.ToList()
                        where fine.DueDate >= Start && fine.DueDate <= End
                        orderby fine.DueDate
                        select fine;

            GenerateReport = true;
            return Page();
        }
    }
}
