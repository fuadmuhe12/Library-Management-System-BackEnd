using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Library_Management_System_BackEnd.Entities.Dtos.AuthorDto
{
    public class AuthorRegisterDto
    {
        [Required]
        required public string Name { get; set; }
        public string? Bio { get; set; } = string.Empty;
    }
}