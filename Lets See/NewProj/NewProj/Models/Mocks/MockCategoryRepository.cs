using NewProj.Classes;
using NewProj.Models.Interfaces;
using NewProj.Models;
using Microsoft.EntityFrameworkCore;

namespace NewProj.Models.Mocks
{
    public class MockCategoryRepository : ICategoryRepository
    {
        private readonly ProjectContext categoryContext;
        public MockCategoryRepository(ProjectContext c)
        {
            categoryContext = c;
        }
        public void Add(Category category)
        {
            categoryContext.Add(category);
            categoryContext.SaveChanges();
        }

        public void Delete(Category category)
        {
            categoryContext.Categories.Remove(category);
            categoryContext.SaveChanges();
        }

        public DbSet<Category> GetCategories()
        {
            return categoryContext.Categories;
        }

        public Category GetCategory(int CategoryId)
        {
            return categoryContext.Categories.Find(CategoryId);
        }

        public async Task SaveAsync()
        {
            await categoryContext.SaveChangesAsync();
        }

        public Category Update(Category category_changes)
        {
            var category = categoryContext.Categories.Attach(category_changes);
            category.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            categoryContext.SaveChanges();
            return category_changes;
        }

      
    }
}
