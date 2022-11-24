using Library.Data;
using Library.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Library.Pages.Employees
{
    public class AssignSupervisorModel : PageModel
    {
        [BindProperty]
        public int? SupervisorID { get; set; }

        public Employee Employee { get; set; } = default!;

        public IList<SelectListItem> SupervisorList { get; set; } = default!;

        private readonly LibraryContext _context;

        public AssignSupervisorModel(LibraryContext context)
        {
            _context = context;
            SupervisorList = new List<SelectListItem>();
            SupervisorList.Add(new SelectListItem()
            {
                Text = "",
                Value = "-1"
            });
        }

        public IActionResult OnGet(int employeeID)
        {
            Employee? e = _context.Employees.FirstOrDefault(e => e.ID == employeeID);
            if (e == null)
            {
                return RedirectToPage("/Employees/Index");
            }
            Employee = e;

            List<Employee> employees = _context.Employees.Where(e => e.ID != Employee.ID && (e.JobRole == Employee.JobRole || e.JobRole == JobRole.Admin)).ToList();
            foreach(var employee in employees)
            {
                SupervisorList.Add(new SelectListItem()
                {
                    Text = $"{employee.FirstName} {employee.LastName}",
                    Value = employee.ID.ToString()
                });
            }

            return Page();
        }

        public IActionResult OnPostAssign(int employeeID)
        {
            Employee? e = _context.Employees.FirstOrDefault(e => e.ID == employeeID);
            if (e == null)
            {
                return RedirectToPage("/Employees/Index");
            }

            if (SupervisorID == -1)
            {
                return RedirectToAction("Get", new { employeeID = employeeID});
            }

            Employee.SupervisorID = SupervisorID;
            
            _context.Attach(e).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();

            return RedirectToPage("/Employees/Index");
        }

        public IActionResult OnPostRemove(int employeeID) 
        {
            Employee? e = _context.Employees.FirstOrDefault(e => e.ID == employeeID);
            if (e == null)
            {
                return RedirectToPage("/Employees/Index");
            }

            e.SupervisorID = null;

            _context.Attach(e).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();

            return RedirectToPage("/Employees/Index");
        }
    }
}
