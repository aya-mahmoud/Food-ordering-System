using NewProj.Classes;
namespace NewProj.Models.Interfaces
{
    public interface IOrderRepository
    {
        Microsoft.EntityFrameworkCore.DbSet<Order> GetOrders();
        Order GetOrder(int OrderId);
        void Add(Order order);
        Order Update(Order order);
        void Delete(Order order);
        Task SaveAsync();
    }
}
