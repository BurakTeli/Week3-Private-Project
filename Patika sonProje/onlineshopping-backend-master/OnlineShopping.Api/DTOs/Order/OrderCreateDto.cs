using System.ComponentModel.DataAnnotations;

namespace OnlineShopping.API.DTOs;

// DTO for creating a new order
public class OrderCreateDto
{
    // Required list of order items
    [Required]
    public List<OrderItemDto> Items { get; set; }
}

// DTO representing a single item in an order
public class OrderItemDto
{
    // Required product ID
    [Required]
    public int ProductId { get; set; }

    // Required quantity for the product
    [Required]
    public int Quantity { get; set; }
}
