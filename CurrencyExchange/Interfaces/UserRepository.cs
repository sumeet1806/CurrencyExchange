using CurrencyExchange.Interfaces;
using CurrencyExchange.Models;
using Newtonsoft.Json;

public class UserRepository : IUserRepository
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public UserRepository(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    // session & dictionary is used to create in memory database. In real scenario sqlconnections
    // or entity framework can be implemented for storing & retrieving data
    private Dictionary<int, User> UserStorage
    {
        get
        {
            var session = _httpContextAccessor.HttpContext.Session;
            var userStorageBytes = session.Get("UserStorage");
            if (userStorageBytes != null)
            {
                var userStorageJson = System.Text.Encoding.UTF8.GetString(userStorageBytes);
                return JsonConvert.DeserializeObject<Dictionary<int, User>>(userStorageJson);
            }
            else
            {
                var userStorage = new Dictionary<int, User>();
                SaveUserStorageToSession(userStorage);
                return userStorage;
            }
        }
    }

    private void SaveUserStorageToSession(Dictionary<int, User> userStorage)
    {
        var session = _httpContextAccessor.HttpContext.Session;
        var userStorageJson = JsonConvert.SerializeObject(userStorage);
        var userStorageBytes = System.Text.Encoding.UTF8.GetBytes(userStorageJson);
        session.Set("UserStorage", userStorageBytes);
    }

    public User GetUserById(int userId)
    {
        return UserStorage.TryGetValue(userId, out User user) ? user : null;

    }

    public void AddUser(User user)
    {
        if (user == null)
        {
            throw new ArgumentNullException(nameof(user));
        }

        var userStorage = UserStorage;
        userStorage[user.UserId] = user;
        SaveUserStorageToSession(userStorage);
    }

    public void UpdateUser(User user)
    {
        if (user == null)
        {
            throw new ArgumentNullException(nameof(user));
        }

        var userStorage = UserStorage;
        userStorage[user.UserId] = user;
        SaveUserStorageToSession(userStorage);
    }

    public void DeleteUser(int userId)
    {
        var userStorage = UserStorage;
        userStorage.Remove(userId);
        SaveUserStorageToSession(userStorage);
    }
}
