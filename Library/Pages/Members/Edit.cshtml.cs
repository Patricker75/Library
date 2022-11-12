using Library.Data;
using Library.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace Library.Pages.Members
{
    [BindProperties]
    public class EditModel : PageModel
    {
        public int ID { get; set; }

        public string FirstName { get; set; } = string.Empty;

        public string MiddleName { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;

        public string PhoneNumber { get; set; } = string.Empty;

        public string Address { get; set; } = string.Empty;

        public int Gender { get; set; } = -1;

        public DateTime BirthDate { get; set; }

        public string ErrorMessage { get; set; } = string.Empty;

        private readonly Library.Data.LibraryContext _context;

        public EditModel(Library.Data.LibraryContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            string? loginType = HttpContext.Session.GetString("loginType");
            
            if (loginType == null || loginType != "member")
            {
                return RedirectToPage("/Index");
            }

            int? memberID = HttpContext.Session.GetInt32("loginID");
            if (memberID == null)
            {
                return RedirectToPage("/Index");
            }

            Member? m = _context.Member.Find(memberID);

            if (m == null)
            {
                return RedirectToPage("/Index");
            }

            ID = m.ID;

            FirstName = m.FirstName;
            MiddleName = m.MiddleName ?? "";
            LastName = m.LastName;

            PhoneNumber = m.PhoneNum ?? "";
            Address = m.Address ?? "";

            Gender = (int)m.Gender;

            BirthDate = m.BirthDate;

            return Page();
        }

        bool VerifyForm()
        {
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

            return true;
        }

        public IActionResult OnPost(int memberID)
        {
            Member? m = _context.Member.Find(memberID);

            if (m == null)
            {
                return RedirectToAction("Get");
            }

            if (VerifyForm())
            {
                m.FirstName = FirstName;
                m.LastName = LastName;

                m.PhoneNum = PhoneNumber;
                m.Address = Address;

                m.Gender = (Gender)Gender;
                m.BirthDate = BirthDate;

                if (!string.IsNullOrEmpty(MiddleName))
                {
                    m.MiddleName = MiddleName.Substring(0, 1);
                }

                _context.SaveChanges();

                return RedirectToPage("/Members/Profile");
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
