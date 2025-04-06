using OnlineShopping.Data.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OnlineShopping.Business.Interfaces
{
    
    public interface IProductService
    {
        
        Task<IEnumerable<Product>> GetAllProductsAsync();

        
        Task<Product> GetProductByIdAsync(int id);

     
        Task<Product> AddProductAsync(Product product);

        
        Task<Product> UpdateProductAsync(Product product);

        
        Task DeleteProductAsync(int id);
    }
}
