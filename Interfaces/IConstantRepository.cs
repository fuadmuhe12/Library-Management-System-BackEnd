using Library_Management_System_BackEnd.Entities.Models;

namespace Library_Management_System_BackEnd.Interfaces
{
    public interface IConstantRepository
    {
        Task<List<Category>> GetCategories();
        Task<List<Tag>> GetTags();
    }
}
