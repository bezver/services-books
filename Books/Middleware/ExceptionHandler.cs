using Books.Domain.DTO;
using Books.Domain.Exceptions;
using Microsoft.Extensions.Primitives;
using Microsoft.Net.Http.Headers;

namespace Books.Middleware
{
    public class ExceptionHandler(RequestDelegate requestDelegate)
    {
        private readonly RequestDelegate _requestDelegate = requestDelegate;

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _requestDelegate(httpContext);
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);

                switch(exception)
                {
                    case ServiceException serviceException:
                        await HandleServiceException(httpContext, serviceException);
                        break;
                    default:
                        await HandleError(httpContext, new Error());
                        break;
                }
            }
        }

        private static async Task HandleServiceException(HttpContext context, ServiceException exception)
        {
            var error = new Error()
            {
                Status = (int)exception.HttpStatusCode,
                Message = exception.Message
            };

            await HandleError(context, error);
            Console.WriteLine(exception.Message);
        }

        private static async Task HandleError(HttpContext context, Error error)
        {
            string contentType = GetContentType(context.Request.Headers.Accept);

            context.Response.StatusCode = error.Status;
            context.Response.ContentType = contentType;

            string responseBody = contentType switch
            {
                "application/xml" => error.ToXml(),
                _ => error.ToJson(),
            };

            await context.Response.WriteAsync(responseBody);
        }

        private static string GetContentType(StringValues acceptHeader)
        {
            var mediaTypes = MediaTypeHeaderValue.ParseList(acceptHeader);
            var xml = mediaTypes.FirstOrDefault(mt => mt.MediaType == "application/xml");
            return xml != null ? "application/xml" : "application/json";
        }
    }
}
