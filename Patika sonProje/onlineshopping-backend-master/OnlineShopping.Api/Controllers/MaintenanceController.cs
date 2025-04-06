using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineShopping.Data.Entities;
using OnlineShopping.Data.Repositories;

namespace OnlineShopping.API.Controllers;

// Defines the controller as an API controller, sets route, and restricts access to Admins only
[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = "Admin")]
public class MaintenanceController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;

    // Constructor to inject unit of work dependency
    public MaintenanceController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    // Activates Maintenance Mode
    [HttpPost("enable")]
    public async Task<IActionResult> EnableMaintenance()
    {
        // Checks if maintenance mode is already active
        var activeMaintenance = (await _unitOfWork.MaintenanceLogRepository
            .FindAsync(m => m.IsInMaintenance))
            .FirstOrDefault();

        if (activeMaintenance != null)
            return BadRequest(new { message = "Maintenance mode is already active." });

        // Creates a new maintenance log entry
        var maintenanceLog = new MaintenanceLog
        {
            IsInMaintenance = true,
            MaintenanceDate = DateTime.UtcNow,
            Description = "Maintenance mode enabled."
        };

        // Adds the new entry and saves changes
        await _unitOfWork.MaintenanceLogRepository.AddAsync(maintenanceLog);
        await _unitOfWork.SaveChangesAsync();

        return Ok(new { message = "Maintenance mode has been enabled." });
    }

    // Deactivates Maintenance Mode
    [HttpPost("disable")]
    public async Task<IActionResult> DisableMaintenance()
    {
        // Retrieves all active maintenance records
        var activeMaintenances = await _unitOfWork.MaintenanceLogRepository
            .FindAsync(m => m.IsInMaintenance);

        if (!activeMaintenances.Any())
            return BadRequest(new { message = "No active maintenance mode found." });

        // Updates each active maintenance record to inactive
        foreach (var maintenance in activeMaintenances)
        {
            maintenance.IsInMaintenance = false;
            _unitOfWork.MaintenanceLogRepository.Update(maintenance);
        }

        // Saves changes to the database
        await _unitOfWork.SaveChangesAsync();

        return Ok(new { message = "Maintenance mode has been disabled." });
    }
}
