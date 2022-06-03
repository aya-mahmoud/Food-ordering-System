using Microsoft.EntityFrameworkCore;
using NewProj.Classes;
using NewProj.Models.Interfaces;

namespace NewProj.Models.Mocks
{
    public class MockUserRepository : IUserRepository
    {
        private readonly ProjectContext UserContext;
        public MockUserRepository(ProjectContext c)
        {
            UserContext = c;
        }
        public void Add(AspNetUser User)
        {
            UserContext.Add(User);
            UserContext.SaveChanges();
        }

        public void Delete(AspNetUser User)
        {
            UserContext.AspNetUsers.Remove(User);
            UserContext.SaveChanges();
        }

        public AspNetUser GetUser(int UserId)
        {
            return UserContext.AspNetUsers.Find(UserId);
        }

        public DbSet<AspNetUser> GetUsers()
        {
            return UserContext.AspNetUsers;
        }

        public async Task SaveAsync()
        {
            await UserContext.SaveChangesAsync();
        }

        public AspNetUser Update(AspNetUser User_changes)
        {
            var user = UserContext.AspNetUsers.Attach(User_changes);
            user.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            UserContext.SaveChanges();
            return User_changes;
        }
    }
}
