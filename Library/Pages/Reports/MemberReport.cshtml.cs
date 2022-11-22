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
        [BindProperty]
        [DataType(DataType.Date)]
        public DateTime Start { get; set; }

        [BindProperty]
        [DataType(DataType.Date)]
        public DateTime End { get; set; }

        public bool GenerateReport = false;
        public IList<Member> Members { get; set; } = default!;

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
            Members = _context.Members.Where(b => Start <= b.JoinDate && b.JoinDate <= End).ToList();
            GenerateReport = true;
            return Page();
        }
    }
}
