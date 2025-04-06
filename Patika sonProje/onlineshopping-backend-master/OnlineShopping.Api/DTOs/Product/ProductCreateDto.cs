using System.ComponentModel.DataAnnotations;

namespace OnlineShopping.API.DTOs;

// DTO for creating a new product
public class ProductCreateDto
{
    // Name of the product (required)
    [Required]
    public string ProductName { get; set; }

    // Price of the product (required)
    [Required]
    public decimal Price { get; set; }

    // Quantity available in stock (required)
    [Required]
    public int StockQuantity { get; set; }
}
