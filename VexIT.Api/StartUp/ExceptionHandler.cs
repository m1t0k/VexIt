using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Serilog;
using VexIT.DataContracts.Exceptions;
using VexIT.DataContracts.V1.Common;

namespace VexIT.Api.StartUp
{
    /// <summary>
    /// </summary>
    public class ExceptionHandler
    {
        /// <summary>
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public static async Task Handle(HttpContext context)
        {
            var ex = context.Features.Get<IExceptionHandlerFeature>();
            var statusCode = (int) HttpStatusCode.InternalServerError;
            var errMessage = "";
            var dtoResponse = new ResponseDto {Success = false};
            if (ex != null)
            {
                switch (ex.Error)
                {
                    case BusinessException _:
                        statusCode = (int) HttpStatusCode.InternalServerError;
                        errMessage = ex.Error.Message;
                        break;

                    case ArgumentNullException _:
                        statusCode = (int) HttpStatusCode.BadRequest;
                        errMessage = "Bad Request.";
                        break;

                    case NotFoundException _:
                        errMessage = ex.Error != null ? ex.Error.Message : "";
                        statusCode = (int) HttpStatusCode.NotFound;
                        break;

                    default:
                        errMessage = "Internal server error.";
                        statusCode = (int) HttpStatusCode.InternalServerError;

                        break;
                }

                var err = $"Error: {ex.Error.InnerException?.Message} \n {ex.Error.Message}\n {ex.Error.StackTrace}";

                Log.Logger.Error(err);
            }

            dtoResponse.Message = errMessage;
            context.Response.Headers.Add("Access-Control-Allow-Origin", "*");
            context.Response.StatusCode = statusCode;
            context.Response.ContentType = "application/json";
            await context.Response.WriteAsync(JsonConvert.SerializeObject(dtoResponse)).ConfigureAwait(false);
        }
    }
}