using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using OnlineShopping.Data.Repositories;
using System.Linq;
using System.Threading.Tasks;
using System;

namespace OnlineShopping.API.Middlewares;

// Middleware to check if the system is under maintenance
public class MaintenanceMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<MaintenanceMiddleware> _logger;

    // Constructor with dependency injection
    public MaintenanceMiddleware(RequestDelegate next, ILogger<MaintenanceMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

   
    public async Task InvokeAsync(HttpContext context, IUnitOfWork unitOfWork)
    {
       
        var activeMaintenance = (await unitOfWork.MaintenanceLogRepository
            .FindAsync(m => m.IsInMaintenance))
            .OrderByDescending(m => m.MaintenanceDate)
            .FirstOrDefault();

        if (activeMaintenance != null)
        {
          
            _logger.LogWarning("Application is in maintenance mode: {Description}", activeMaintenance.Description);

            context.Response.StatusCode = StatusCodes.Status503ServiceUnavailable;
            context.Response.ContentType = "application/json";

            await context.Response.WriteAsJsonAsync(new
            {
                message = "The system is currently under maintenance. Please try again later.",
                description = activeMaintenance.Description,
                maintenanceDate = activeMaintenance.MaintenanceDate
            });

            return;
        }

       
        await _next(context);
    }
}
