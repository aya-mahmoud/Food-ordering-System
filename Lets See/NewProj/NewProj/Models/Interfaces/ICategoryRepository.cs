using NewProj.Classes;
namespace NewProj.Models.Interfaces
{
    public interface ICategoryRepository
    {
        Microsoft.EntityFrameworkCore.DbSet<Category> GetCategories();
        Category GetCategory(int CategoryId);
        void Add(Category category);
        Category Update(Category category);
        void Delete(Category category);
        Task SaveAsync();
    }
}
