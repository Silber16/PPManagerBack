using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PPManager.Models;
using PPManager.ViewModels;
using System.Security.Claims;


namespace PPManager.Controllers
{
    public class AccountController : Controller
    {
        private readonly SignInManager<MyUser> _signInManager;
        private readonly UserManager<MyUser> _userManager;
        private readonly ILogger<AccountController> _logger;


        public AccountController(

                UserManager<MyUser> userManager,
                SignInManager<MyUser> signInManager,
                ILogger<AccountController> logger
            )
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> Register([FromBody] RegisterDTO RegisterInput)
        {
            if (!ModelState.IsValid)
            {
                foreach (var modelStateEntry in ModelState.Values)
                {
                    foreach (var error in modelStateEntry.Errors)
                    {
                        Console.WriteLine($"Error: {error.ErrorMessage}");
                    }
                }
                return BadRequest(ModelState);
            }

            var user = new MyUser { UserName = RegisterInput.Email, Email = RegisterInput.Email };
            var result = await _userManager.CreateAsync(user, RegisterInput.Password);
            if (result.Succeeded)
            {
                await _signInManager.SignInAsync(user, isPersistent: true);
                return Ok(result);
            }
            foreach (var error in result.Errors)
            {
                Console.WriteLine(error.Description);
                ModelState.AddModelError(string.Empty, error.Description);
            }

            return BadRequest(ModelState);
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginDTO loginInput, string? returnUrl = null)
        {
            returnUrl = returnUrl ?? Url.Content("~/");


            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(
                    loginInput.Email,
                    loginInput.Password, 
                    isPersistent: loginInput.RememberMe, 
                    lockoutOnFailure: false);
                if (result.Succeeded)
                {
                    _logger.LogInformation("User logged in.");
                    return LocalRedirect(returnUrl);

                }
                ModelState.AddModelError(string.Empty, "Invalid login attempt.");

                return BadRequest();
            }
            return BadRequest();
        }

        [HttpPost("/Account/LogOut")]
        public async Task<IActionResult> OnPost()
        {
            await _signInManager.SignOutAsync();
            _logger.LogInformation("User logged out.");

            return Ok(); ;
        }

        [HttpGet("/Account/CheckAuth")]
        public IActionResult CheckAuth()
            {
                if (User.Identity.IsAuthenticated)
                {
                    return Ok(new { isAuthenticated = true });
                }
                else
                {
                    return Ok(new { isAuthenticated = false });
                }

            }

        [HttpGet("/Account/GetUserInfo")]
        public async Task<IActionResult> GetUserInfo()
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

                if (userId != null)
                {
                    var user = await _userManager.FindByIdAsync(userId);

                    if (user == null)
                    {
                        return NotFound();
                    }

                    var userData = new
                    {
                        user.UserName
                    };

                    return Ok(userData);
                }

                return NotFound("User ID not found");
                   
            }

    }
}
