using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Library_Management_System_BackEnd.Helper.Json;
using Microsoft.VisualBasic;

namespace Library_Management_System_BackEnd.Entities.Dtos.BookDto
{
    public class UpdateBookDto
    {
        [Required]
        public string UpdatedTitle { get; set; } = string.Empty;

        [Required]
        public int UpdatedAuthorId { get; set; }

        [Required]
        public string UpdatedISBN { get; set; } = string.Empty;

        [Required]
        public int UpdatedCategoryId { get; set; }

        [Required]
        public DateOnly UpdatedPublicationYear { get; set; } 
        [Required]
        public string? PrevioesCoverImagePath { get; set; }

        [Required]
        public string UpdatedDescription { get; set; } = string.Empty;
        public required IFormFile? UpdatedCoverImage { get; set; }

    }
}
