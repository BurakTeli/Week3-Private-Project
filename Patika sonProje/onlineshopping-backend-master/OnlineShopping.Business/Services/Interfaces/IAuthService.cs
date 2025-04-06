using OnlineShopping.Data.Entities;
using System.Threading.Tasks;

namespace OnlineShopping.Business.Interfaces
{
   
    public interface IAuthService
    {
      
        Task<string> GenerateJwtTokenAsync(User user);
    }
}
