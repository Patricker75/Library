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
        public IList<MemberReport> Reports { get; set; } = default!;

        private LibraryContext _context;
        
        public MemberReportModel(LibraryContext context)
        {
            _context = context;
        }

        public void OnGet()
        {
            Start = DateTime.Today.AddDays(-30);
            End = DateTime.Today;
           // if (_context.MemberReports != null)
            //{
             //   Reports = _context.MemberReports.ToList();
            //}

        }

        public IActionResult OnPost()
        {
            End = End.AddDays(1).AddSeconds(-1);
            if (!ModelState.IsValid)
            {
                return Page();
            }
            
            Reports = _context.MemberReports.Where(vr => Start <= vr.JoinDate && vr.JoinDate <= End).ToList();
            GenerateReport = true;
            return Page();
        }

    }
}
