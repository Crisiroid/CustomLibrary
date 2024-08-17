using CustomLibrary.Data;
using CustomLibrary.Interfaces;
using CustomLibrary.Models;

namespace CustomLibrary.Services
{
    public class UserService : IUserService
    {
        private readonly LibraryDBContext _dbContext; 

        public UserService(LibraryDBContext dbContext)
        {
            _dbContext = dbContext;
        }
        public void CreateUser(User user)
        {
            _dbContext.Users.Add(user);
            _dbContext.SaveChanges();
        }

        public void DeleteUser(User user)
        {
            _dbContext.Users.Remove(user);
            _dbContext.SaveChanges();
        }

        public User FindUserByEmail(string email)
        {
            return _dbContext.Users.FirstOrDefault(x => x.Email == email);
        }

        public User FindUserById(int id)
        {
            return _dbContext.Users.Find(id);
        }

        public IEnumerable<User> GetAllUsers()
        {
            return _dbContext.Users.ToList();
        }

        public void UpdateUser(User user)
        {
            _dbContext.Users.Update(user);
            _dbContext.SaveChanges();
        }
        public User FindUserByEmailAndPassword(string email, string password)
        {
            return _dbContext.Users.SingleOrDefault(u => u.Email == email && u.Password == password);
        }
    }
}
