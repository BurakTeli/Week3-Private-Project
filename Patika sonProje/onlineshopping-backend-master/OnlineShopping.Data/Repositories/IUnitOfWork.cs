using System;
using System.Threading.Tasks;
using OnlineShopping.Data.Entities;

namespace OnlineShopping.Data.Repositories
{
    // UnitOfWork pattern is used to manage multiple repositories under a single context.
    public interface IUnitOfWork : IDisposable
    {
        // Repository for User entity
        IGenericRepository<User> UserRepository { get; }

        // Repository for Product entity
        IGenericRepository<Product> ProductRepository { get; }

        // Repository for Order entity
        IGenericRepository<Order> OrderRepository { get; }

        // Repository for OrderProduct entity
        IGenericRepository<OrderProduct> OrderProductRepository { get; }

        // Repository for MaintenanceLog entity
        IGenericRepository<MaintenanceLog> MaintenanceLogRepository { get; }

        // Retrieves a specific order along with its related details
        Task<Order> GetOrderWithDetailsAsync(int orderId);

        // Retrieves all orders of a specific customer including related details
        Task<IEnumerable<Order>> GetOrdersWithDetailsByCustomerIdAsync(int customerId);

        // Saves all changes made in the context to the database
        Task<int> SaveChangesAsync();
    }
}
