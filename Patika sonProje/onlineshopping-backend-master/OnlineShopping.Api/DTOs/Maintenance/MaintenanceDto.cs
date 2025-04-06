using System.ComponentModel.DataAnnotations;

namespace OnlineShopping.API.DTOs;

public class MaintenanceDto
{
    [Required]
    public bool IsInMaintenance { get; set; }

    public string Description { get; set; }
}
