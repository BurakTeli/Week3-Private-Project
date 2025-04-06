using System;
using System.ComponentModel.DataAnnotations;

namespace OnlineShopping.Data.Entities
{
  
    public class MaintenanceLog
    {
      
        [Key] public int Id { get; set; }

      
        [Required] public DateTime MaintenanceDate { get; set; }

      
        public bool IsInMaintenance { get; set; }

       
        public string Description { get; set; }
    }
}
