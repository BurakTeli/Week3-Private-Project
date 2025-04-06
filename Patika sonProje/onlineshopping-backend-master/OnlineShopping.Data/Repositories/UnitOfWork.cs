using Microsoft.EntityFrameworkCore;
using OnlineShopping.Data.Entities;
using System.Threading.Tasks;

namespace OnlineShopping.Data.Repositories
{
    // Implements the UnitOfWork pattern to coordinate multiple repositories
    public class UnitOfWork : IUnitOfWork
    {
        private readonly OnlineShoppingDbContext _context;
        private IGenericRepository<User> _userRepository;
        private IGenericRepository<Product> _productRepository;
        private IGenericRepository<Order> _orderRepository;
        private IGenericRepository<OrderProduct> _orderProductRepository;
        private IGenericRepository<MaintenanceLog> _maintenanceLogRepository;

        public UnitOfWork(OnlineShoppingDbContext context)
        {
            _context = context;
        }

        // Initializes the UserRepository instance using lazy loading
        public IGenericRepository<User> UserRepository =>
            _userRepository ??= new GenericRepository<User>(_context);

        // Initializes the ProductRepository instance using lazy loading
        public IGenericRepository<Product> ProductRepository =>
            _productRepository ??= new GenericRepository<Product>(_context);

        // Initializes the OrderRepository instance using lazy loading
        public IGenericRepository<Order> OrderRepository =>
            _orderRepository ??= new GenericRepository<Order>(_context);

        // Initializes the OrderProductRepository instance using lazy loading
        public IGenericRepository<OrderProduct> OrderProductRepository =>
            _orderProductRepository ??= new GenericRepository<OrderProduct>(_context);

        // Initializes the MaintenanceLogRepository instance using lazy loading
        public IGenericRepository<MaintenanceLog> MaintenanceLogRepository =>
            _maintenanceLogRepository ??= new GenericRepository<MaintenanceLog>(_context);

        // Retrieves an order with its related products and product details
        public async Task<Order> GetOrderWithDetailsAsync(int orderId)
        {
            return await _context.Orders
                .Include(o => o.OrderProducts)
                    .ThenInclude(op => op.Product)
                .FirstOrDefaultAsync(o => o.Id == orderId);
        }

        // Saves all changes made in the context to the database
        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

        // Properly disposes the DbContext instance
        public void Dispose()
        {
            _context.Dispose();
        }

        // Retrieves all orders for a specific customer along with related product details
        public async Task<IEnumerable<Order>> GetOrdersWithDetailsByCustomerIdAsync(int customerId)
        {
            return await _context.Orders
                .Where(o => o.CustomerId == customerId)
                .Include(o => o.OrderProducts)
                    .ThenInclude(op => op.Product)
                .ToListAsync();
        }
    }
}
