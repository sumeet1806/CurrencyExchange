using CurrencyExchange.Interfaces;
using CurrencyExchange.Models;
using Microsoft.AspNetCore.Mvc;

namespace CurrencyExchange.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;

        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        // POST: api/users/{userId}/balance
        [HttpPost("{userId}/balance/Create")]
        public ActionResult CreateUserBalance(int userId, decimal initialBalance)
        {
            var user = _userRepository.GetUserById(userId);
            if (user == null)
            {
                User newUser = new User();
                newUser.UserId = userId;
                newUser.Balance = initialBalance;
                _userRepository.AddUser(newUser);
            }
            else
            {
                user.Balance = initialBalance;
                _userRepository.UpdateUser(user);
            }

            return Ok($"Initial Balance for user with userid {userId} is {initialBalance}");
        }

        // GET: api/users/{userId}/balance        
        [HttpGet("{userId}/balance/Get")]
        public ActionResult<decimal> GetUserBalance(int userId)
        {
            var user = _userRepository.GetUserById(userId);
            if (user == null)
            {
                return NotFound();
            }

            return Ok($"Balance for user with userid {user.UserId} is {user.Balance}");
        }        

        // PUT: api/users/{userId}/balance
        [HttpPut("{userId}/balance/Update")]
        public ActionResult UpdateUserBalance(int userId, decimal newBalance)
        {
            var user = _userRepository.GetUserById(userId);
            if (user == null)
            {
                return NotFound();
            }

            user.Balance = newBalance;
            _userRepository.UpdateUser(user);

            return Ok($"Updated Balance for user with userid {user.UserId} is {user.Balance}");
        }
    }
}
