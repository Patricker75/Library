using Library.Data;
using Library.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Library.Pages.Reports
{
    public class LibraryMembersModel : PageModel
    {
        public List<Member> Members { get; set; } = default!;

        public readonly LibraryContext Context;

        public LibraryMembersModel(LibraryContext context)
        {
            Context = context;
        }

        // If Suspended is True, Check members with suspended
        public void OnGet(bool[] types, bool suspended)
        {
            List<MemberType> memberTypes = new List<MemberType>();

            for (int i = 0; i < types.Length; i++)
            {
                if (types[i])
                {
                    memberTypes.Add((MemberType)i);
                }
            }

            if (suspended)
            {
                Members = Context.Member.Where(m => memberTypes.Contains(m.MemberType) && m.MemberStatus != MemberStatus.Inactive).ToList();
            }
            else
            {
                Members = Context.Member.Where(m => memberTypes.Contains(m.MemberType) && m.MemberStatus == MemberStatus.Active).ToList();
            }
        }
    }
}
