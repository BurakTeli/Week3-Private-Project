using OnlineShopping.Data.Entities;
using System.Threading.Tasks;

namespace OnlineShopping.Business.Interfaces
{
   
    public interface IOrderService
    {
       
        Task<Order> CreateOrderAsync(Order order);

        
        Task<Order> GetOrderByIdAsync(int id);

       
        Task<IEnumerable<Order>> GetOrdersByCustomerIdAsync(int customerId);

        
        Task UpdateOrderAsync(Order order);
    }
}
