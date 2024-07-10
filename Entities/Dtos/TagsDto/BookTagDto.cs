using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Library_Management_System_BackEnd.Entities.Models;

namespace Library_Management_System_BackEnd.Entities.Dtos.TagsDto
{
    public class BookTagDto
    {
        public Collection<BookTag> BookTags { get; set; } = new Collection<BookTag>();
    }
}