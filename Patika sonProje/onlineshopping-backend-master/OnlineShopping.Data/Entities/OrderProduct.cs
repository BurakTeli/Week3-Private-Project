using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace OnlineShopping.Data.Entities
{
   
    public class OrderProduct
    {
      
        
        public int OrderId { get; set; }

       
        
        public int ProductId { get; set; }

       
        
        public int Quantity { get; set; }

        
        [JsonIgnore]
        public virtual Order Order { get; set; }

        
        
        public virtual Product Product { get; set; }
    }
}
