using CustomLibrary.Models;

namespace CustomLibrary.Interfaces
{
    public interface IUserService
    {
        IEnumerable<User> GetAllUsers();
        User FindUserById(int id);
        User FindUserByEmail(string email);
        User FindUserByEmailAndPassword(string email, string password);
        void CreateUser(User user);
        void DeleteUser(User user);
        void UpdateUser(User user);

    }
}
