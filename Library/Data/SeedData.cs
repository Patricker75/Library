using Microsoft.EntityFrameworkCore;
using Library.Models;

namespace Library.Data
{
    public class SeedData
    {
        public static void Initialize(IServiceProvider service)
        {
            using (var context = new LibraryContext(service.GetRequiredService<DbContextOptions<LibraryContext>>()))
            {
                if (context == null)
                {
                    throw new ArgumentNullException("Null LibraryContext");
                }

                if (!context.Member.Any())
                {
                    SeedMembers(context.Member);
                }

                context.SaveChanges();
            }
        }

        static void SeedMembers(DbSet<Member> members)
        {
            members.AddRange(
                new Member
                {
                    Username = "JDoe198",
                    FirstName = "John",
                    LastName = "Doe",
                    Gender = Gender.Male,
                    BirthDate = new DateTime(1970, 1, 1),
                    MemberType = MemberType.Professional
                },
                new Member
                {
                    Username = "JRSmith",
                    FirstName = "Jake",
                    MiddleName = "R",
                    LastName = "Smith",
                    Gender = Gender.Male,
                    BirthDate = new DateTime(2000, 12, 31),
                    MemberType = MemberType.Student
                },
                new Member
                {
                    Username = "HPerry",
                    FirstName = "Helen",
                    LastName = "Perry",
                    Gender = Gender.Female,
                    BirthDate = new DateTime(1983, 4, 1),
                    MemberType = MemberType.Professional
                },
                new Member
                {
                    Username = "VBui3",
                    FirstName = "Viet",
                    LastName = "Bui",
                    Gender = Gender.Male,
                    BirthDate = new DateTime(2002, 3, 4),
                    MemberType = MemberType.Student
                }   
            );
        }
    }
}
