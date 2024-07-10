using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Library_Management_System_BackEnd.Entities.Dtos.BookDto
{
    public class CreateBookDto
    {
        [Required]
        public string Title { get; set; } = string.Empty;

        [Required]
        public int AuthorId { get; set; }
        public string ISBN { get; set; } = string.Empty;

        [Required]
        public int CategoryId { get; set; }

        [Required]
        public DateOnly PublicationYear { get; set; }
        public string Description { get; set; } = string.Empty;

        [Required]
        public required IFormFile CoverImage { get; set; }
    }
}
