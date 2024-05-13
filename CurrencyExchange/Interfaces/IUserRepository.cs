using CurrencyExchange.Models;

namespace CurrencyExchange.Interfaces
{
    public interface IUserRepository
    {
        User GetUserById(int userId);
        void UpdateUser(User user);
        void AddUser(User user);
    }
}
