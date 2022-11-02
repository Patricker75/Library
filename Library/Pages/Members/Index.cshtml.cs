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

namespace Library.Pages.Members
{
    public class IndexModel : PageModel
    {
        private readonly Library.Data.LibraryContext _context;


        [BindProperty]
        public string Username { get; set; } = string.Empty;

        [BindProperty,MaxLength(10)]
        public string Password { get; set; } = string.Empty;

        [BindProperty]
        public string FirstName { get; set; } = string.Empty;

        [BindProperty]
        public string MiddleName { get; set; } = string.Empty;

        [BindProperty]
        public string LastName { get; set; } = string.Empty;

        [BindProperty]
        public string PhoneNum { get; set; } = string.Empty;

        [BindProperty]
        public string Address { get; set; } = string.Empty;

        [BindProperty]
        public int Gender { get; set; } = -1;
        [BindProperty]
        public DateTime BirthDate { get; set; }

        [BindProperty]
        public DateTime JoinDate { get; set; }

        [BindProperty]
        public int MemberType { get; set; } = -1;

        public IndexModel(Library.Data.LibraryContext context)
        {
            _context = context;
        }

        public IList<Member> Member { get;set; } = default!;

      
    }
}
