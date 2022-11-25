using Library.Data;
using Library.Models.Views;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using NuGet.Versioning;
using System.ComponentModel.DataAnnotations;

namespace Library.Pages.Reports
{
    public class UsageReportModel : PageModel
    {
        [DisplayFormat(DataFormatString = "{0:mm/dd/yyy}")]
        [DataType(DataType.Date)]
        [BindProperty]
        public DateTime StartDate { get; set; }

        [DisplayFormat(DataFormatString = "{0:mm/dd/yyy}")]
        [DataType(DataType.Date)]
        [BindProperty]
        public DateTime EndDate { get; set; }

        [BindProperty]
        public int ReportType { get; set; }

        public IEnumerable<IGrouping<string, ServiceUsageReport>> ServiceReport { get; set; } = default!;
        public IEnumerable<IGrouping<string, ResourceUsageReport>> ResourceReport { get; set; } = default!;

        public string Header { get; set; } = string.Empty;

        private readonly LibraryContext _context;

        public UsageReportModel(LibraryContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            string? role = HttpContext.Session.GetString("employeeRole");
            if (role == null || role != "Admin") 
            {
                return RedirectToPage("/Index");
            }

            ReportType = -1;
            StartDate = DateTime.Today.AddDays(-30);
            EndDate = DateTime.Today;

            return Page();
        }

        public void OnPost() 
        {
            EndDate = EndDate.AddDays(1).AddSeconds(-1);
            if (ReportType == 0)
            {
                Header = $"Service Report from {StartDate:MM/dd/yyy} to {EndDate:MM/dd/yyy}";
                ServiceReport = from use in _context.ServiceUsageReports.ToList()
                                where use.TimeStamp >= StartDate && use.TimeStamp <= EndDate
                                group use by use.Name;
            }
            else if (ReportType == 1)
            {
                Header = $"Resource Report from {StartDate:MM/dd/yyy} to {EndDate:MM/dd/yyy}";
                ResourceReport = from access in _context.ResourceUsageReports.ToList()
                                      where access.TimeStamp >= StartDate && access.TimeStamp <= EndDate
                                      group access by access.Name;
            }
        }
    }
}
