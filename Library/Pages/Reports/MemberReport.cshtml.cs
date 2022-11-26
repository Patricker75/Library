using Library.Data;
using Library.Models.Relationships;
using Library.Models.Views;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace Library.Pages.Reports
{
    public class MemberReportModel : PageModel
    {
        [BindProperty]
        [DataType(DataType.Date)]
        public DateTime Start { get; set; }

        [BindProperty]
        [DataType(DataType.Date)]
        public DateTime End { get; set; }

        public bool GenerateReport = false;

        public IOrderedEnumerable<MemberReport> Reporting { get; set; } = default!;

        private LibraryContext _context;
        
        public MemberReportModel(LibraryContext context)
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

            Reporting = from report in _context.MemberReports.ToList()
                        where report.JoinDate >= Start && report.JoinDate <= End
                        orderby report.JoinDate
                        select report;

            GenerateReport = true;
            return Page();
        }

    }
}
