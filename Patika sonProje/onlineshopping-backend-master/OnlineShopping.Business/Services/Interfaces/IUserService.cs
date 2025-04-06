using OnlineShopping.Data.Entities;

namespace OnlineShopping.Business.Interfaces
{
  
    public interface IUserService
    {
        
        Task<User> RegisterUserAsync(User user);

       
        Task<User> AuthenticateUserAsync(string email, string password);

        
        Task<User> UpdateUserAsync(User user);

        
        Task<User> GetUserByIdAsync(int userId);
    }
}
