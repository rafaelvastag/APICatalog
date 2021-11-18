using APICatalog.Extensions.Model;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace APICatalog.Extensions
{
    public static class ApiExceptionsMiddlewareExtensions
    {

        public static void ConfigureExceptionHandler(this IApplicationBuilder app)
        {
            app.UseExceptionHandler( appError =>
            {
                appError.Run( async context =>
                {
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    context.Response.ContentType = "application/json";

                    var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                    if (null != contextFeature)
                    {
                        await context.Response.WriteAsync(new ErrorDetails()
                        {
                            StatusCode = context.Response.StatusCode, 
                            Message = contextFeature.Error.Message ,
                            Trace = contextFeature.Error.StackTrace
                        }.ToString());
                    }
                });
            });
        }

    }
}
