namespace OnlineShopping.API.DTOs;

// DTO for returning order details
public class OrderDto
{
    // Order ID
    public int Id { get; set; }

    // Date when the order was placed
    public DateTime OrderDate { get; set; }

    // Total price of the order
    public decimal TotalAmount { get; set; }

    // List of items in the order with detailed information
    public List<OrderItemDetailDto> Items { get; set; }
}

// DTO representing detailed information for a single item in an order
public class OrderItemDetailDto
{
    // ID of the product
    public int ProductId { get; set; }

    // Name of the product
    public string ProductName { get; set; }

    // Price per unit of the product
    public decimal UnitPrice { get; set; }

    // Quantity ordered
    public int Quantity { get; set; }
}
