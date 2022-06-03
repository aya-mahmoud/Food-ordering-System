using NewProj.Classes;
namespace NewProj.Models.Interfaces
{
    public interface ICartRepository
    {
        Microsoft.EntityFrameworkCore.DbSet<Cart> GetCarts();
        Cart GetCart(int CartId);
        void Add(Cart cart);
        Cart Update(Cart cart);
        void Delete(Cart cart);
        Task SaveAsync();
    }
}
