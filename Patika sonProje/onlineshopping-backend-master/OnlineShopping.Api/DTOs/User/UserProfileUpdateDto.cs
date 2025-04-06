namespace OnlineShopping.API.DTOs;

// DTO for returning user profile information
public class UserProfileDto
{
    // Unique identifier of the user
    public int Id { get; set; }

    // User's first name
    public string FirstName { get; set; }

    // User's last name
    public string LastName { get; set; }

    // User's email address
    public string Email { get; set; }

    // User's phone number
    public string PhoneNumber { get; set; }
}
