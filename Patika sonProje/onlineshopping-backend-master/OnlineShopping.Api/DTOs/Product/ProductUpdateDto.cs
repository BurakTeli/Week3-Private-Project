using System.ComponentModel.DataAnnotations;

namespace OnlineShopping.API.DTOs;

// DTO for updating an existing product
public class ProductUpdateDto
{
    // ID of the product to update (required)
    [Required]
    public int Id { get; set; }

    // Updated name of the product (required)
    [Required]
    public string ProductName { get; set; }

    // Updated price of the product (required)
    [Required]
    public decimal Price { get; set; }

    // Updated stock quantity of the product (required)
    [Required]
    public int StockQuantity { get; set; }
}
