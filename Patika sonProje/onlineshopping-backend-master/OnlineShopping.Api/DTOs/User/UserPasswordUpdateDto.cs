using System.ComponentModel.DataAnnotations;

namespace OnlineShopping.API.DTOs;

// DTO for updating a user's password
public class UserPasswordUpdateDto
{
    // The user's current password (required)
    [Required]
    public string CurrentPassword { get; set; }

    // The new password the user wants to set (required, minimum 6 characters)
    [Required, MinLength(6)]
    public string NewPassword { get; set; }

    // Re-entered new password to confirm it matches (required, must match NewPassword)
    [Required, Compare("NewPassword", ErrorMessage = "Yeni şifreler eşleşmiyor.")]
    public string ReNewPassword { get; set; }
}
