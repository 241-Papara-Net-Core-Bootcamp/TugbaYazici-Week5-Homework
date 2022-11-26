using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Diagnostics;

namespace PaparaThirdWeek.Api.Filters
{
    public class LogAttribute : ActionFilterAttribute
    {
        public LogAttribute()
        {

        }
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            Trace.WriteLine($"Action Method {context.ActionDescriptor.DisplayName}" +
                $"executing at {DateTime.Now.ToShortDateString()}");
        }
        public override void OnActionExecuted(ActionExecutedContext context)
        {
            Trace.WriteLine($"Action Method {context.ActionDescriptor.DisplayName}" +
                $"executed at {DateTime.Now.ToShortDateString()}");
        }
    }
}
