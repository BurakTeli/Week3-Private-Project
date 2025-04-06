using OnlineShopping.Business.Interfaces;
using OnlineShopping.Data.Entities;
using OnlineShopping.Data.Repositories;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineShopping.Business.Services;

public class UserService : IUserService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IPasswordEncryptionService _encryptionService;

    public UserService(IUnitOfWork unitOfWork, IPasswordEncryptionService encryptionService)
    {
        _unitOfWork = unitOfWork;
        _encryptionService = encryptionService;
    }

 
    public async Task<User> RegisterUserAsync(User user)
    {
      
        user.Password = _encryptionService.Encrypt(user.Password);

   
        await _unitOfWork.UserRepository.AddAsync(user);
        await _unitOfWork.SaveChangesAsync();
        return user;
    }


    public async Task<User> AuthenticateUserAsync(string email, string password)
    {
        var users = await _unitOfWork.UserRepository.FindAsync(u => u.Email == email);
        var user = users.FirstOrDefault();

        if (user != null)
        {
            // Decrypt the password and compare
            var decryptedPassword = _encryptionService.Decrypt(user.Password);
            if (decryptedPassword == password)
                return user;
        }

        return null;
    }

      public async Task<User> UpdateUserAsync(User user, bool updatePassword = false)
    {
        if (updatePassword)
            user.Password = _encryptionService.Encrypt(user.Password);

        _unitOfWork.UserRepository.Update(user);
        await _unitOfWork.SaveChangesAsync();
        return user;
    }

        public async Task<User> UpdateUserAsync(User user)
    {
       
        user.Password = _encryptionService.Encrypt(user.Password);

        _unitOfWork.UserRepository.Update(user);
        await _unitOfWork.SaveChangesAsync();
        return user;
    }

    
    public async Task<User> GetUserByIdAsync(int userId)
    {
        return await _unitOfWork.UserRepository.GetByIdAsync(userId);
    }
}
