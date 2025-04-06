namespace OnlineShopping.Business.Interfaces
{
    
    public interface IPasswordEncryptionService
    {
       
        string Encrypt(string plainText);

      
        string Decrypt(string cipherText);
    }
}
