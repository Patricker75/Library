using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Library.Data;
using Library.Models;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;
using System.Threading.Tasks.Dataflow;

namespace Library.Pages.Members
{
    public class AddModel : PageModel
    {
        [BindProperty]
        public Member NewMember { get; set; } = default!;

        public int Error { get; set; }

        private readonly LibraryContext _context;

        public AddModel(LibraryContext context)
        {
            _context = context;
        }

        public void OnGet()
        {
        }

        bool VerifyForm()
        {
            // Check if username exists in member or employee table
            if (_context.Member.Where(m => m.Username == NewMember.Username).Any() || _context.Employee.Where(e => e.Username == NewMember.Username).Any()) 
            {
                Error = 1;
                return false;
            }

            if (NewMember.BirthDate > NewMember.JoinDate)
            {
                Error = 2;
                return false;
            }

            return true;
        }

        public IActionResult OnPost()
        {
            var errors = ModelState.Values.SelectMany(v => v.Errors);
            NewMember.JoinDate = DateTime.Today;

            if (ModelState.IsValid && VerifyForm())
            {
                switch (NewMember.MemberType)
                {
                    case MemberType.Student:
                        NewMember.CheckOutLimit = 3;
                        break;

                    case MemberType.Professional:
                        NewMember.CheckOutLimit = 10;
                        break;
                }

                _context.Member.Add(NewMember);
                _context.SaveChanges();

                return RedirectToPage("/Login");
            }

            return Page();
        }      
    }
}
