using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineShopping.API.DTOs;
using OnlineShopping.Business.Interfaces;
using OnlineShopping.Data.Entities;

namespace OnlineShopping.API.Controllers;


[ApiController]
[Route("api/[controller]")]
public class ProductController : ControllerBase
{
    private readonly IProductService _productService;

   
    public ProductController(IProductService productService)
    {
        _productService = productService;
    }

   
    [HttpGet]
    [AllowAnonymous]
    public async Task<IActionResult> GetAll()
    {
        var products = await _productService.GetAllProductsAsync();
        var productDtos = products.Select(p => new ProductDto
        {
            Id = p.Id,
            ProductName = p.ProductName,
            Price = p.Price,
            StockQuantity = p.StockQuantity
        });

        return Ok(productDtos);
    }

    
    [HttpGet("{id}")]
    [AllowAnonymous]
    public async Task<IActionResult> GetById(int id)
    {
        var product = await _productService.GetProductByIdAsync(id);
        if (product is null)
            return NotFound();

        var productDto = new ProductDto
        {
            Id = product.Id,
            ProductName = product.ProductName,
            Price = product.Price,
            StockQuantity = product.StockQuantity
        };

        return Ok(productDto);
    }

    // ONLY ADMIN can add a new product
    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Add([FromBody] ProductCreateDto productDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var product = new Product
        {
            ProductName = productDto.ProductName,
            Price = productDto.Price,
            StockQuantity = productDto.StockQuantity
        };

        var createdProduct = await _productService.AddProductAsync(product);
        return CreatedAtAction(nameof(GetById), new { id = createdProduct.Id }, createdProduct);
    }

    
    [HttpPut("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Update(int id, [FromBody] ProductUpdateDto productDto)
    {
        if (id != productDto.Id)
            return BadRequest("Product IDs do not match.");

        var existingProduct = await _productService.GetProductByIdAsync(id);
        if (existingProduct is null)
            return NotFound();

        existingProduct.ProductName = productDto.ProductName;
        existingProduct.Price = productDto.Price;
        existingProduct.StockQuantity = productDto.StockQuantity;

        var updatedProduct = await _productService.UpdateProductAsync(existingProduct);
        return Ok(updatedProduct);
    }

    
    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Delete(int id)
    {
        await _productService.DeleteProductAsync(id);
        return NoContent();
    }
}
