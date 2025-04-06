using System.ComponentModel.DataAnnotations;

namespace OnlineShopping.API.DTOs;

// DTO for updating an existing order
public class OrderUpdateDto
{
    // ID of the order to be updated (required)
    [Required]
    public int OrderId { get; set; }

    // List of updated order items (required)
    [Required]
    public List<OrderItemDto> Items { get; set; }
}
