using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APICatalog.Filters
{
    public class ApiLoggingFilter : IActionFilter
    {

        private readonly ILogger<ApiLoggingFilter> _logger;

        public ApiLoggingFilter(ILogger<ApiLoggingFilter> logger)
        {
            _logger = logger;
        }

        //execute before Action
        public void OnActionExecuting(ActionExecutingContext context)
        {
            _logger.LogInformation("### Running -> OnActionExecuting");
            _logger.LogInformation("###################################################");
            _logger.LogInformation($"{DateTime.Now.ToLongTimeString()}");
            _logger.LogInformation($"ModelState : {context.ModelState.IsValid}");
            _logger.LogInformation("###################################################");
        }
        //execute after Action
        public void OnActionExecuted(ActionExecutedContext context)
        {
            _logger.LogInformation("### Running -> OnActionExecuted");
            _logger.LogInformation("###################################################");
            _logger.LogInformation($"{DateTime.Now.ToLongTimeString()}");
            _logger.LogInformation("###################################################");
        }
    }
}
