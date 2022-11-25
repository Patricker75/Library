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
        //public IList<MemberReport> Reports { get; set; } = default!;

        public IEnumerable<IGrouping<string, MemberReport>> Reporting { get; set; } = default!;

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
             //  var Reportss = from rep in Reports
              //            orderby rep.JoinDate
               //           select rep;
                         
            //}
            //foreach(var std in Reportss)
           // Reports = Reportss;
        }

        public IActionResult OnPost()
        {
            End = End.AddDays(1).AddSeconds(-1);
            if (!ModelState.IsValid)
            {
                return Page();
            }
            Reporting = from rep in _context.MemberReports.ToList()
                      where rep.JoinDate >= Start && rep.JoinDate <= End
                      group rep by rep.FirstName;
            // Reports = Reportss;
            //Reports = _context.MemberReports.Where(vr => Start <= vr.JoinDate && vr.JoinDate <= End).ToList();

            GenerateReport = true;
            return Page();
        }

    }
}
