using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Library_Management_System_BackEnd.Entities.Dtos.BookDto;
using Library_Management_System_BackEnd.Entities.Dtos.TagsDto;
using Library_Management_System_BackEnd.Entities.Models;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Library_Management_System_BackEnd.Entities.Mapper
{
    public static class TagsMapper
    {
        public static ViewTagDto MapToViewTagDto(this Tag tag)
        {
            return new ViewTagDto { TagName = tag.TagName };
        }

        public static BookTagDto MapToBookTagDto(this AddTagsDto addTagsDto, int bookId)
        {
            var bookTagDto = new BookTagDto();
            foreach (int tagId in addTagsDto.TagId)
            {
                bookTagDto.BookTags.Add(new BookTag { BookId = bookId, TagId = tagId });
            }
            return bookTagDto;
        }

        public static ListOfTagsDto MapToTagsList(this ICollection<BookTag>? bookTags)
        {
            var Tags = new ListOfTagsDto();
            foreach (BookTag tag in bookTags!)
            {
                Tags.Tags!.Add(tag.Tag!.TagName);
            }
            return Tags;
        }
    }
}
