using Library.Models;
using Microsoft.EntityFrameworkCore;

namespace Library.Data
{
    public static class SeedData
    {
        public static void Init(IServiceProvider service)
        {
            using (var context = new LibraryContext(service.GetRequiredService<DbContextOptions<LibraryContext>>()))
            {
                Book newBook = new Book()
                {
                    Title = "Test",
                    DeweyNumber = "100.953 BUI",
                    Summary = "sum",
                    Genre = "Fiction",
                    Audience = (Audience)2,
                    Condition = (Condition)1
                };

                context.Book.Add(newBook);

                context.SaveChanges();
            };
        }
    }
}
