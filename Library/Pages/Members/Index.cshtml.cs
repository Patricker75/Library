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


        public string ErrorMessage { get; set; } = string.Empty;

        public IndexModel(Library.Data.LibraryContext context)
        {
            _context = context;
        }

        bool VerifyForm()
        {
            if (string.IsNullOrEmpty(Username))
            {
                return false;
            }

            if (string.IsNullOrEmpty(Password))
            {
                return false;
            }

            if (string.IsNullOrEmpty(FirstName))
            {
                return false;
            }
            if (string.IsNullOrEmpty(LastName))
            {
                return false;
            }

            if (string.IsNullOrEmpty(PhoneNum))
            {
                return false;
            }
            if (string.IsNullOrEmpty(Address))
            {
                return false;
            }
            if (Gender <0)
            {
                return false;
            }

            return true;



        }
        public IActionResult OnPost()
        {
            if (VerifyForm())
            {
                Member newMember = new Member()
                {
                    Username = Username,
                    Password = Password,
                    FirstName = FirstName,
                    LastName = LastName,
                    PhoneNum = PhoneNum,
                    Address = Address,
                    Gender = (Gender)Gender,

                    
                };

                _context.Member.Add(newMember);
                _context.SaveChanges();

                return RedirectToPage("Create");
            }
            else
            {
                ErrorMessage = "A Required Field has Been Left Blank";

                ModelState.Clear();

                return Page();
            }
        }

        public IList<Member> Member { get;set; } = default!;

      
    }
}
