using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Library_Management_System_BackEnd.Entities.Models;
using Library_Management_System_BackEnd.Helper.Query;

namespace Library_Management_System_BackEnd.Interfaces
{
    public interface IAuthorRepository
    {
        Task<List<Author>> GetAllAuthors(AuthorQuery query);
        Task<Author?> GetAuthorById(int id);
        Task<Author> AddAuthor(Author author);
        Task<Author> UpdateAuthor(Author newAuthor);
        Task<bool> AuthorExists(int id);
        Task<int> DeleteAuthor(int id);
    }
}
