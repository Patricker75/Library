using Library.Data;
using Library.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Library.Pages.Reports
{
    public class MemberReportModel : PageModel
    {
        //public Report Report { get; set; } = default!;

        [BindProperty]
        [DataType(DataType.Date)]
        public DateTime Start { get; set; }

        [BindProperty]
        [DataType(DataType.Date)]
        public DateTime End { get; set; }

        public bool GenerateReport = false;
        public IList<Member> Members { get; set; } = default!;

        public IList<Report> Reports { get; set; } = default!;

        private LibraryContext _context;
        public MemberReportModel(LibraryContext context)
        {
            _context = context;
        }
        public IActionResult OnGet()
        {
            if (_context.Reports != null)
            {
                Reports = _context.Reports.ToList();
            }

            return Page();
        }
        //public void OnGet()
        //{
         //   Start = DateTime.Today.AddDays(-30);
          //  End = DateTime.Today;

       // }

        public IActionResult OnPost()
        {

            End = End.AddDays(1).AddSeconds(-1);
            if (!ModelState.IsValid)
            {
                return Page();
            }
            //Members = _context.Members.Where(b => Start <= b.JoinDate && b.JoinDate <= End).ToList();
            //Reports = _context.Reports.Where(b => Start <= b.JoinDate && b.JoinDate <= End).ToList();
            GenerateReport = true;
            return Page();
        }
    }
}
