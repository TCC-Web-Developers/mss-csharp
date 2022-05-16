using System.Net;
using System.Text.Json;

namespace StarterAPI.Middleware
{

    //https://jasonwatmore.com/post/2022/01/17/net-6-global-error-handler-tutorial-with-example
    public class ErrorHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public ErrorHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception error)
            {
                var response = context.Response;
                response.ContentType = "application/json";

                switch (error)
                {
                    
                    case KeyNotFoundException e:
                        // not found error
                        response.StatusCode = (int)HttpStatusCode.NotFound;
                        break;
                    default:
                        // unhandled error
                        response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        break;
                }

                var result = JsonSerializer.Serialize(new { 
                    message = error?.Message,
                    description = error?.InnerException?.Message ?? string.Empty,
                });


                await response.WriteAsync(result);

                //TODO: Do some system logging of the other exception details like stack strace, timestamp
            }
        }
    }
}
