using NewProj.Classes;
using NewProj.Models.Interfaces;
using NewProj.Models;
using Microsoft.EntityFrameworkCore;

namespace NewProj.Models.Mocks
{
    public class MockProductRepository : IProductRepository
    {
        private readonly ProjectContext ProductContext;
        public MockProductRepository(ProjectContext c)
        {
            ProductContext = c;
        }
        public void Add(Product product)
        {
            ProductContext.Add(product);
            ProductContext.SaveChanges();
        }

        public void Delete(Product product)
        {
            ProductContext.Products.Remove(product);
            ProductContext.SaveChanges();
        }

        public Product GetProduct(int productId)
        {
            return ProductContext.Products.FirstOrDefault(x => x.ID == productId);
        }

        public DbSet<Product> GetProducts()
        {
            return ProductContext.Products;
        }

        public async Task SaveAsync()
        {
            await ProductContext.SaveChangesAsync();
        }

        public Product Update(Product product_changes)
        {
            var product = ProductContext.Products.Attach(product_changes);
            product.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            ProductContext.SaveChanges();
            return product_changes;
        }
    }
}
