using NewProj.Classes;
namespace NewProj.Models.Interfaces
{
    public interface IUserRepository
    {
        Microsoft.EntityFrameworkCore.DbSet<AspNetUser> GetUsers();
        AspNetUser GetUser(int UserId);
        void Add(AspNetUser User);
        AspNetUser Update(AspNetUser User);
        void Delete(AspNetUser User);
        Task SaveAsync();
    }
}
