using NewProj.Classes;
using NewProj.Models.Interfaces;
using NewProj.Models;
using Microsoft.EntityFrameworkCore;

namespace NewProj.Models.Mocks
{
    public class MockOrderRepository : IOrderRepository
    {
        private readonly ProjectContext orderContext;
        public MockOrderRepository(ProjectContext c)
        {
            orderContext = c;
        }
        public void Add(Order order)
        {
           
            orderContext.Add(order);
            orderContext.SaveChanges();
        }

        public void Delete(Order order)
        {
            orderContext.Orders.Remove(order);
            orderContext.SaveChanges();
        }

        public Order GetOrder(int OrderId)
        {
            return orderContext.Orders.FirstOrDefault(x => x.ID == OrderId);
        }

        public DbSet<Order> GetOrders()
        {
            return orderContext.Orders;
        }

        public async Task SaveAsync()
        {
            await orderContext.SaveChangesAsync();
        }

        public Order Update(Order order_changes)
        {
            var order = orderContext.Orders.Attach(order_changes);
            order.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            orderContext.SaveChanges();
            return order_changes;
        }
    }
}
