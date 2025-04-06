using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace OnlineShopping.API.Filters;

public class AccessTimeFilterAttribute : ActionFilterAttribute
{
    private readonly TimeSpan _startTime;
    private readonly TimeSpan _endTime;

    // Örneğin: "08:00", "20:00"
    public AccessTimeFilterAttribute(string startTime, string endTime)
    {
        _startTime = TimeSpan.Parse(startTime);
        _endTime = TimeSpan.Parse(endTime);
    }

    public override void OnActionExecuting(ActionExecutingContext context)
    {
        var currentTime = DateTime.UtcNow.TimeOfDay;

        if (currentTime < _startTime || currentTime > _endTime)
        {
            context.Result = new ContentResult
            {
                StatusCode = 403,
                ContentType = "application/json",
                Content = $"{{ \"error\": \"Bu işlem sadece {_startTime:hh\\:mm} ile {_endTime:hh\\:mm} (UTC) arasında yapılabilir.\" }}"
            };
        }

        base.OnActionExecuting(context);
    }
}
