using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineShopping.API.DTOs;
using OnlineShopping.Business.Interfaces;
using System.Security.Claims;

namespace OnlineShopping.API.Controllers;


[ApiController]
[Route("api/[controller]")]
[Authorize]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    
    public UserController(IUserService userService)
    {
        _userService = userService;
    }

   
    [HttpGet("profile")]
    public async Task<IActionResult> GetProfile()
    {
        var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
        var user = await _userService.GetUserByIdAsync(userId);

        if (user == null)
            return NotFound();

        var profileDto = new UserProfileDto
        {
            Id = user.Id,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Email = user.Email,
            PhoneNumber = user.PhoneNumber
        };

        return Ok(profileDto);
    }

    // Update the profile of the currently authenticated user
    [HttpPut("profile")]
    public async Task<IActionResult> UpdateProfile([FromBody] UserProfileUpdateDto updateDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
        var user = await _userService.GetUserByIdAsync(userId);

        if (user == null)
            return NotFound();

        // Update user's profile fields
        user.FirstName = updateDto.FirstName;
        user.LastName = updateDto.LastName;
        user.PhoneNumber = updateDto.PhoneNumber;

        await _userService.UpdateUserAsync(user);

        return Ok(new { message = "Profile updated successfully." });
    }

    // Update the password of the currently authenticated user
    [HttpPut("update-password")]
    public async Task<IActionResult> UpdatePassword([FromBody] UserPasswordUpdateDto updateDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
        var user = await _userService.GetUserByIdAsync(userId);

        if (user == null)
            return NotFound();

        // Verify the current password
        var isAuthenticated = await _userService.AuthenticateUserAsync(user.Email, updateDto.CurrentPassword);
        if (isAuthenticated == null)
            return BadRequest(new { error = "Current password is incorrect." });

        // Set new password (assumed to be encrypted with data protection)
        user.Password = updateDto.NewPassword;

        await _userService.UpdateUserAsync(user);

        return Ok(new { message = "Password updated successfully." });
    }
}
