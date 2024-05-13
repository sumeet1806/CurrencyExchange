using CurrencyExchange.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly IConfiguration _configuration;

    public AuthController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IConfiguration configuration)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _configuration = configuration;
    }

    [HttpPost("signup")]
    public async Task<IActionResult> SignUp(SignUpRequest model)
    {        
        var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
        //Note : Code will fail at below line as there is no valid sql connection string added in appsettings.json
        var result = await _userManager.CreateAsync(user, model.Password);

        if (result.Succeeded)
        {                        
            return Ok("User created successfully");
        }
        else
        {            
            return BadRequest(result.Errors);
        }
    }

    [HttpPost("signin")]
    public async Task<IActionResult> SignIn(SignInRequest model)
    {
        //Note : Code will fail at below line as there is no valid sql connection string added in appsettings.json
        var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, false, false);

        if (result.Succeeded)
        {                     
            return Ok("User authenticated successfully");
        }
        else
        {            
            return Unauthorized();
        }
    }
}
