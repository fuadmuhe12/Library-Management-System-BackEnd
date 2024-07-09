using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace Library_Management_System_BackEnd.Models
{
    public class Book
    {
        public int BookId { get; set; }
        public string Title { get; set; } = string.Empty;
        public int AuthorId { get; set; }
        public Author? Author { get; set; }
        public string ISBN { get; set; } = string.Empty;
        public int CategoryId { get; set; }
        public Category? Category { get; set; }
        public BookStatus BookStatus { get; set; }
        public DateOnly PublicationYear { get; set; }
        public string Description { get; set; } = string.Empty;
        public string CoverImage { get; set; } = string.Empty;
    }
}
