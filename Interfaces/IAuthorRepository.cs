using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Library_Management_System_BackEnd.Models;

namespace Library_Management_System_BackEnd.Interfaces
{
    public interface IAuthorRepository
    {
        Task<List<Author>> GetAllAuthors();
        Task<Author?> GetAuthorById(int id);
        Task<Author> AddAuthor(Author author);
        
    }
}