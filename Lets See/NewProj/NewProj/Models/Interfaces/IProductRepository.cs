using NewProj.Classes;
namespace NewProj.Models.Interfaces
{
    public interface IProductRepository
    {
        Microsoft.EntityFrameworkCore.DbSet<Product> GetProducts();
        Product GetProduct(int productId);
        void Add(Product product);
        Product Update(Product product);
        void Delete(Product product);
        Task SaveAsync();
    }
}
