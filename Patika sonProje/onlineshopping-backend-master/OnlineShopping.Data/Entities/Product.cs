using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OnlineShopping.Data.Entities
{
   
    public class Product
    {
       
       
        [Key] public int Id { get; set; }

       
        [Required] public string ProductName { get; set; }

       
        
        [Required] public decimal Price { get; set; }

       
        
        [Required] public int StockQuantity { get; set; }

        
        
        public virtual ICollection<OrderProduct> OrderProducts { get; set; }
    }
}
