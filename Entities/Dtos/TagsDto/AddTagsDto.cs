using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace Library_Management_System_BackEnd.Entities.Dtos.TagsDto
{
    public class AddTagsDto
    {
        public Collection<int> TagId { get; set; } = new Collection<int>();
    }
}
