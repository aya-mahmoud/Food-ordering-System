using NewProj.Classes;
using NewProj.Models.Interfaces;
using NewProj.Models;

namespace NewProj.Models.Mocks
{
    public class MockCartRepository : ICartRepository
    {
        private readonly ProjectContext CartContext;
        public MockCartRepository(ProjectContext c)
        {
            CartContext = c;
        }
        public void Add(Cart cart)
        {
            CartContext.Add(cart);
            CartContext.SaveChanges();
        }

        public void Delete(Cart cart)
        {
            CartContext.Carts.Remove(cart);
            CartContext.SaveChanges();
        }

        public Cart GetCart(int CartId)
        {
            return CartContext.Carts.Find(CartId);
        }

        public Microsoft.EntityFrameworkCore.DbSet<Cart> GetCarts()
        {
            return CartContext.Carts;
        }

        public async Task SaveAsync()
        {
            await CartContext.SaveChangesAsync();
        }

        public Cart Update(Cart cart_changes)
        {
            var cart = CartContext.Carts.Attach(cart_changes);
            cart.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            CartContext.SaveChanges();
            return cart_changes;
        }

       
    }
}
