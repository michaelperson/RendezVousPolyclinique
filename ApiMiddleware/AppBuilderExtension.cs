using ApiTools.Logging;
using ApiTools.Logging.Interfaces;
using ApiTools.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ApiMiddleware
{
   public static class AppExtensions
    {

        public static void AddGlobalErrorHandlerWithLog(this IApplicationBuilder app, ILoggerManager loggerManager)
        {
            app.UseExceptionHandler(
              appError =>
              {
                  LoggerManager manager = new LoggerManager();
                  appError.Run(async context =>
                  {
                      context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                      context.Response.ContentType = "application/json";
                      var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                      if (contextFeature != null)
                      {
                          manager.LogError($"Something went wrong: {contextFeature.Error}");
                          await context.Response.WriteAsync(new ErrorDetails() { StatusCode = context.Response.StatusCode, Message = "Internal Server Error." }.ToString());
                      }
                  });
              });
        }
    }
}
