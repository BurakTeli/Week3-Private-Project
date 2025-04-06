using System.ComponentModel.DataAnnotations;
namespace OnlineShopping.API.DTOs;

public class UserRegisterDto
{
    [Required, EmailAddress]
    public string Email { get; set; }

    [Required, MinLength(6)]
    public string Password { get; set; }

    [Required, Compare("Password", ErrorMessage = "Şifreler eşleşmiyor.")]
    public string RePassword { get; set; }
}
