using Microsoft.EntityFrameworkCore;
using OnlineShopping.Business.Interfaces;
using OnlineShopping.Data.Entities;
using OnlineShopping.Data.Repositories;
using System.Threading.Tasks;

namespace OnlineShopping.Business.Services
{
  
    public class OrderService : IOrderService
    {
        private readonly IUnitOfWork _unitOfWork;

        public OrderService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

       
        public async Task<Order> CreateOrderAsync(Order order)
        {
            foreach (var orderProduct in order.OrderProducts)
            {
                var product = await _unitOfWork.ProductRepository.GetByIdAsync(orderProduct.ProductId);

                if (product == null)
                    throw new Exception($"Product not found: {orderProduct.ProductId}");

                if (product.StockQuantity < orderProduct.Quantity)
                    throw new Exception($"Insufficient stock: {product.ProductName}");

               
                product.StockQuantity -= orderProduct.Quantity;

                _unitOfWork.ProductRepository.Update(product);
            }

            
            await _unitOfWork.OrderRepository.AddAsync(order);
            await _unitOfWork.SaveChangesAsync();

            return order;
        }

        
        public async Task<Order> GetOrderByIdAsync(int id)
        {
            return await _unitOfWork.GetOrderWithDetailsAsync(id);
        }

        
        public async Task<IEnumerable<Order>> GetOrdersByCustomerIdAsync(int customerId)
        {
            return await _unitOfWork.GetOrdersWithDetailsByCustomerIdAsync(customerId);
        }

       
        public async Task UpdateOrderAsync(Order updatedOrder)
        {
            var existingOrder = await _unitOfWork.GetOrderWithDetailsAsync(updatedOrder.Id);

            if (existingOrder == null)
                throw new Exception("Order not found.");

         
            foreach (var oldOrderItem in existingOrder.OrderProducts)
            {
                var product = await _unitOfWork.ProductRepository.GetByIdAsync(oldOrderItem.ProductId);
                product.StockQuantity += oldOrderItem.Quantity;
                _unitOfWork.ProductRepository.Update(product);
            }

            
            existingOrder.OrderProducts.Clear();

            
            foreach (var newOrderItem in updatedOrder.OrderProducts)
            {
                var product = await _unitOfWork.ProductRepository.GetByIdAsync(newOrderItem.ProductId);

                if (product == null)
                    throw new Exception($"Product not found: {newOrderItem.ProductId}");

                if (product.StockQuantity < newOrderItem.Quantity)
                    throw new Exception($"Insufficient stock: {product.ProductName}");

                product.StockQuantity -= newOrderItem.Quantity;
                existingOrder.OrderProducts.Add(newOrderItem);
                _unitOfWork.ProductRepository.Update(product);
            }

         
            existingOrder.TotalAmount = updatedOrder.TotalAmount;

 
            _unitOfWork.OrderRepository.Update(existingOrder);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
