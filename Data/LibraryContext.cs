using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Library_Management_System_BackEnd.Models;
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
        public Roles MyProperty { get; set; }
    }
}
