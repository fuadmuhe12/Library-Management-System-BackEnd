using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Library_Management_System_BackEnd.Entities.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Library_Management_System_BackEnd.Data
{
    public class LibraryContext(DbContextOptions options) : IdentityDbContext(options)
    {
        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<Fine> Fines { get; set; }
        public DbSet<BorrowingRecord> BorrowingRecords { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            //seed for roles
            var roles = new IdentityRole[]
            {
                new()
                {
                    Name = LMSUserRoles.Admin.ToString(),
                    NormalizedName = LMSUserRoles.Admin.ToString().ToUpper()
                },
                new()
                {
                    Name = LMSUserRoles.User.ToString(),
                    NormalizedName = LMSUserRoles.User.ToString().ToUpper()
                }
            };
            builder.Entity<IdentityRole>().HasData(roles);

            //seed for categories

            var categories = new Category[]
            {
                new() { CategoryId = 1, CategoryName = "Mystery" },
                new() { CategoryId = 2, CategoryName = "Fantasy" },
                new() { CategoryId = 3, CategoryName = "Science Fiction" },
                new() { CategoryId = 4, CategoryName = "Biography" },
                new() { CategoryId = 5, CategoryName = "Self-Help" },
                new() { CategoryId = 6, CategoryName = "Travel" },
                new() { CategoryId = 7, CategoryName = "Picture Books" },
                new() { CategoryId = 8, CategoryName = "Textbooks" },
                new() { CategoryId = 9, CategoryName = "Comics" },
                new() { CategoryId = 10, CategoryName = "Cooking" }
            };

            builder.Entity<Category>().HasData(categories);
        }
    }
}
