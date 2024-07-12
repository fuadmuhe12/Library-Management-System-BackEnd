using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Library_Management_System_BackEnd.Entities.Dtos.TagsDto;

namespace Library_Management_System_BackEnd.Entities.Dtos.BookDto
{
    public class ViewBookDto
    {
        public int BookId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string AuthorName { get; set; } = string.Empty;
        public string ISBN { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
        public string BookStatus { get; set; } = string.Empty;
        public DateOnly PublicationYear { get; set; }
        public string Description { get; set; } = string.Empty;
        public string CoverImage { get; set; } = string.Empty;
        public ListOfTagsDto? Tags { get; set; } 
    }
}
