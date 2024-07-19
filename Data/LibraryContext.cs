using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Library_Management_System_BackEnd.Entities.Dtos.AuthDto;
using Library_Management_System_BackEnd.Entities.Mapper;
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
        public DbSet<Tag> Tags { get; set; }
        public DbSet<BookTag> BookTag { get; set; }

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
                new() { CategoryName = "Fiction", CategoryId = 1 },
                new() { CategoryName = "Non_Fiction", CategoryId = 2 },
                new() { CategoryName = "Children", CategoryId = 3 },
                new() { CategoryName = "Young Adult", CategoryId = 4 },
                new() { CategoryName = "Academic", CategoryId = 5 }
            };

            builder.Entity<Category>().HasData(categories);
            // seed for tags

            var tags = new Tag[]
            {
                new() { TagId = 1, TagName = "Adventure" },
                new() { TagId = 2, TagName = "Mystery" },
                new() { TagId = 3, TagName = "Romance" },
                new() { TagId = 4, TagName = "Science_Fiction" },
                new() { TagId = 5, TagName = "Fantasy" },
                new() { TagId = 6, TagName = "Thriller" },
                new() { TagId = 7, TagName = "Historical" },
                new() { TagId = 8, TagName = "Coming_of_Age" },
                new() { TagId = 9, TagName = "Bestsellers" },
                new() { TagId = 10, TagName = "New_Arrivals" },
                new() { TagId = 11, TagName = "Award_Winners" },
                new() { TagId = 12, TagName = "Ebook" },
                new() { TagId = 13, TagName = "Audiobook" },
                new() { TagId = 14, TagName = "Hardcover" },
                new() { TagId = 15, TagName = "Paperback" },
                new() { TagId = 16, TagName = "English" },
                new() { TagId = 17, TagName = "Mental_Health" },
                new() { TagId = 18, TagName = "Environmental" }
            };
            builder.Entity<Tag>().HasData(tags);

            builder.Entity<BookTag>().HasKey(bookTag => new { bookTag.BookId, bookTag.TagId });

            builder
                .Entity<BookTag>()
                .HasOne(bookTag => bookTag.Tag)
                .WithMany(tag => tag.BookTags)
                .HasForeignKey(bookTag => bookTag.TagId);
            builder
                .Entity<BookTag>()
                .HasOne(bookTag => bookTag.Book)
                .WithMany(book => book.BookTags)
                .HasForeignKey(bookTag => bookTag.BookId);

            // Fines and Borrowing Records
            builder
                .Entity<Fine>()
                .HasOne(fine => fine.BorrowingRecord)
                .WithMany()
                .HasForeignKey(fine => fine.BorrowingRecordId)
                .OnDelete(DeleteBehavior.Restrict); // Prevent cascade delete
        }
    }
}
