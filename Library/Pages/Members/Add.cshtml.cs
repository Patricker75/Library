﻿using System;
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
    [BindProperties]
    public class AddModel : PageModel
    {
        public string Username { get; set; } = string.Empty;

        public string Password { get; set; } = string.Empty;

        public string FirstName { get; set; } = string.Empty;

        [MaxLength(1)]
        public string MiddleName { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;

        public string PhoneNumber { get; set; } = string.Empty;

        public string Address { get; set; } = string.Empty;

        public int Gender { get; set; } = -1;

        public DateTime BirthDate { get; set; }

        public DateTime JoinDate { get; set; } = DateTime.Now;

        public int MemberType { get; set; } = -1;

        public string ErrorMessage { get; set; } = string.Empty;

        private readonly Library.Data.LibraryContext _context;

        public AddModel(Library.Data.LibraryContext context)
        {
            _context = context;
        }

        public void OnGet()
        {
            BirthDate = new DateTime(DateTime.Today.Year - 4, DateTime.Today.Month, DateTime.Today.Day);
        }

        bool VerifyForm()
        {
            if (string.IsNullOrEmpty(Username) || Username.Length > 20)
            {
                return false;
            }
            // Check if username exists in member or employee table
            if (_context.Member.Where(m => m.Username == Username).Any() || _context.Employee.Where(e => e.Username == Username).Any()) 
            {
                return false;
            }

            if (string.IsNullOrEmpty(Password) || Password.Length > 10)
            {
                return false;
            }
            if (string.IsNullOrEmpty(FirstName) || FirstName.Length > 20)
            {
                return false;
            }
            if (string.IsNullOrEmpty(LastName) || LastName.Length > 20)
            {
                return false;
            }
            if (string.IsNullOrEmpty(PhoneNumber) || PhoneNumber.Length > 10)
            {
                return false;
            }
            if (string.IsNullOrEmpty(Address) || Address.Length > 150)
            {
                return false;
            }
            
            if (Gender < 0)
            {
                return false;
            }

            if (MemberType < 0)
            {
                return false;
            }

            if (BirthDate > JoinDate)
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
                    
                    PhoneNum = PhoneNumber,

                    Address = Address,
                    Gender = (Gender)Gender,

                    BirthDate = BirthDate,
                    JoinDate = JoinDate,

                    MemberType = (MemberType)MemberType,

                    MemberStatus = MemberStatus.Active,
                    Balance = 0,
                    CheckOutCount = 0
                };

                switch (newMember.MemberType)
                {
                    case Data.MemberType.Student:
                        newMember.CheckOutLimit = 3;
                        break;

                    case Data.MemberType.Professional:
                        newMember.CheckOutLimit = 10;
                        break;
                }

                if (!string.IsNullOrEmpty(MiddleName))
                {
                    newMember.MiddleName = MiddleName.Substring(0,1);
                }

                _context.Member.Add(newMember);
                _context.SaveChanges();

                return RedirectToPage("Add");
            }
            else
            {
                ErrorMessage = "A Required Field has Been Left Blank or Invalid Length/Value";

                ModelState.Clear();

                return Page();
            }
        }      
    }
}
