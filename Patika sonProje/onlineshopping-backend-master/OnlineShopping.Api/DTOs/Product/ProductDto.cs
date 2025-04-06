namespace OnlineShopping.API.DTOs;

// DTO for returning product information
public class ProductDto
{
    // Unique identifier of the product
    public int Id { get; set; }

    // Name of the product
    public string ProductName { get; set; }

    // Price of the product
    public decimal Price { get; set; }

    // Quantity available in stock
    public int StockQuantity { get; set; }
}
