using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace OnlineShopping.Data.Entities
{
    // Represents an order placed by a customer

    public class Order
    {
     

        [Key] public int Id { get; set; }

    

        [Required] public DateTime OrderDate { get; set; }

       

        [Required] public decimal TotalAmount { get; set; }

        

        public int CustomerId { get; set; }

        

        [ForeignKey("CustomerId")]
        public virtual User Customer { get; set; }

        

        [JsonIgnore]
        public virtual ICollection<OrderProduct> OrderProducts { get; set; }
    }
}
