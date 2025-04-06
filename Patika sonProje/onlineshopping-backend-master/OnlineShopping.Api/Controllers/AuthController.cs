using Microsoft.AspNetCore.Mvc;
using OnlineShopping.Business.Interfaces;
using OnlineShopping.Data.Entities;
using OnlineShopping.API.DTOs;
using Microsoft.AspNetCore.Authorization;

namespace OnlineShopping.API.Controllers;

// Defines the controller as an API controller and sets the route for HTTP requests
[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly IAuthService _authService;

    // Constructor to inject user and authentication services
    public AuthController(IUserService userService, IAuthService authService)
    {
        _userService = userService;
        _authService = authService;
    }

    // Handles user registration
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] UserRegisterDto request)
    {
        // Returns bad request if the model state is not valid
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        // Creates a new user with default values
        var user = new User
        {
            Email = request.Email,
            Password = request.Password,
            Role = UserRole.Customer, // Automatically assigns Customer role
            FirstName = "Yeni",
            LastName = "Kullanıcı",
            PhoneNumber = "Belirtilmedi"
        };

        // Registers the user using the user service
        var createdUser = await _userService.RegisterUserAsync(user);
        return Ok(new { createdUser.Id, createdUser.Email });
    }

    // Handles user login
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] UserLoginDto request)
    {
        // Returns bad request if the model state is not valid
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        // Authenticates the user
        var user = await _userService.AuthenticateUserAsync(request.Email, request.Password);
        if (user is null)
            return Unauthorized(new { message = "Geçersiz kullanıcı adı veya şifre." });

        // Generates JWT token for the authenticated user
        var token = await _authService.GenerateJwtTokenAsync(user);
        return Ok(new { token });
    }

    // Handles user logout (on client-side only, since JWT is stateless)
    [HttpPost("logout")]
    [Authorize]
    public IActionResult Logout()
    {
        // This method can act as a trigger to invalidate the JWT token on the client side
        return Ok(new { message = "Başarıyla çıkış yapıldı." });
    }
}
