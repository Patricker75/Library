using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Library.Data;
using Library.Models;

namespace Library.Pages.Employees
{
    public class IndexModel : PageModel
    {
        public IList<Employee> Employees { get; set; } = default!;

        public readonly LibraryContext Context;

        public IndexModel(LibraryContext context)
        {
            Context = context;
        }

        public void OnGet()
        {
            if (Context.Employee != null)
            {
                Employees = Context.Employee.ToList();
            }
        }

        public IActionResult OnPostEdit(int employeeID)
        {
            return RedirectToPage("/Employees/Edit", new { employeeID = employeeID });
        }
    }
}
