using Library.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Linq;

namespace Library.Pages.Employees
{
    [BindProperties]
    public class MembersRecordModel : PageModel
    {
        public bool CheckSuspended { get; set; }

        public MemberType[] Types = Enum.GetValues(typeof(MemberType)).Cast<MemberType>().ToArray();
        public bool[] CheckTypes { get; set; } = new bool[Enum.GetNames(typeof(MemberType)).Length];
        
        public void OnGet()
        {
        }

        private bool VerifyForm()
        {
            bool isValid = false;
            foreach (bool b in CheckTypes)
            {
                if (b)
                {
                    isValid = b;
                }
            }

            return isValid;
        }

        public IActionResult OnPost()
        {
            if (VerifyForm())
            {
                return RedirectToPage("/Reports/LibraryMembers", new { types = CheckTypes, suspended = CheckSuspended });
            }
            else
            {
                return Page();
            }
        }
    }
}
