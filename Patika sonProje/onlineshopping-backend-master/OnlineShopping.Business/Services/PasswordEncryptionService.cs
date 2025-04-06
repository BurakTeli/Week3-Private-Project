using Microsoft.AspNetCore.DataProtection;
using OnlineShopping.Business.Interfaces;

namespace OnlineShopping.Business.Services
{
  
    public class PasswordEncryptionService : IPasswordEncryptionService
    {
        private readonly IDataProtector _protector;

       
        public PasswordEncryptionService(IDataProtectionProvider dataProtectionProvider)
        {
           
            _protector = dataProtectionProvider.CreateProtector("PasswordProtector");
        }

       
        public string Encrypt(string plainText)
        {
            return _protector.Protect(plainText);
        }

      
        public string Decrypt(string cipherText)
        {
            return _protector.Unprotect(cipherText);
        }
    }
}
