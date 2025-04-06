using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineShopping.API.DTOs;
using OnlineShopping.API.Filters;
using OnlineShopping.Business.Interfaces;
using OnlineShopping.Data.Entities;
using System.Security.Claims;

namespace OnlineShopping.API.Controllers;


[ApiController]
[Route("api/[controller]")]
[Authorize]
public class OrderController : ControllerBase
{
    private readonly IOrderService _orderService;
    private readonly IProductService _productService;

        public OrderController(IOrderService orderService, IProductService productService)
    {
        _orderService = orderService;
        _productService = productService;
    }

    
    [HttpPost]
    [Authorize]
    [AccessTimeFilter("08:00", "17:00")] 
    public async Task<IActionResult> CreateOrder([FromBody] OrderCreateDto orderCreateDto)
    {
       
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

       
        var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
        var orderItems = new List<OrderProduct>();
        decimal totalAmount = 0;

       
        foreach (var item in orderCreateDto.Items)
        {
            var product = await _productService.GetProductByIdAsync(item.ProductId);
            if (product == null)
                return BadRequest($"Product not found: {item.ProductId}");

            totalAmount += product.Price * item.Quantity;

            orderItems.Add(new OrderProduct
            {
                ProductId = product.Id,
                Quantity = item.Quantity
            });
        }

        
        var order = new Order
        {
            CustomerId = userId,
            OrderDate = DateTime.UtcNow,
            TotalAmount = totalAmount,
            OrderProducts = orderItems
        };

        try
        {
            
            var createdOrder = await _orderService.CreateOrderAsync(order);
            return Ok(createdOrder);
        }
        catch (Exception ex)
        {
           
            return BadRequest(new { error = ex.Message });
        }
    }

    
    [HttpGet("myorders")]
    public async Task<IActionResult> GetMyOrders()
    {
        var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
        var orders = await _orderService.GetOrdersByCustomerIdAsync(userId);

        // Maps orders to DTOs
        var orderDtos = orders.Select(o => new OrderDto
        {
            Id = o.Id,
            OrderDate = o.OrderDate,
            TotalAmount = o.TotalAmount,
            Items = o.OrderProducts.Select(op => new OrderItemDetailDto
            {
                ProductId = op.ProductId,
                ProductName = op.Product.ProductName,
                UnitPrice = op.Product.Price,
                Quantity = op.Quantity
            }).ToList()
        });

        return Ok(orderDtos);
    }

    
    [HttpGet("{orderId}")]
    public async Task<IActionResult> GetOrderById(int orderId)
    {
        var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
        var order = await _orderService.GetOrderByIdAsync(orderId);

        // Ensures the order exists and belongs to the user
        if (order == null || order.CustomerId != userId)
            return Unauthorized();

        // Maps to DTO
        var orderDto = new OrderDto
        {
            Id = order.Id,
            OrderDate = order.OrderDate,
            TotalAmount = order.TotalAmount,
            Items = order.OrderProducts.Select(op => new OrderItemDetailDto
            {
                ProductId = op.ProductId,
                ProductName = op.Product.ProductName,
                UnitPrice = op.Product.Price,
                Quantity = op.Quantity
            }).ToList()
        };

        return Ok(orderDto);
    }

   
    [HttpPut("{orderId}")]
    public async Task<IActionResult> UpdateOrder(int orderId, [FromBody] OrderUpdateDto updateDto)
    {
  
        if (orderId != updateDto.OrderId)
            return BadRequest("Order IDs do not match.");

        var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
        var existingOrder = await _orderService.GetOrderByIdAsync(orderId);

        if (existingOrder is null)
            return NotFound();

        
        if (existingOrder.CustomerId != userId)
            return Unauthorized("You are not authorized to update this order.");

        existingOrder.OrderProducts.Clear();
        decimal newTotalAmount = 0;

     
        foreach (var item in updateDto.Items)
        {
            var product = await _productService.GetProductByIdAsync(item.ProductId);
            if (product == null)
                return BadRequest($"Product not found: {item.ProductId}");

            newTotalAmount += product.Price * item.Quantity;

            existingOrder.OrderProducts.Add(new OrderProduct
            {
                OrderId = existingOrder.Id,
                ProductId = product.Id,
                Quantity = item.Quantity
            });
        }

        // Updates the total amount of the order
        existingOrder.TotalAmount = newTotalAmount;

        // Saves the updated order
        await _orderService.UpdateOrderAsync(existingOrder);

        return Ok(new { message = "Order updated successfully.", existingOrder.Id });
    }
}
