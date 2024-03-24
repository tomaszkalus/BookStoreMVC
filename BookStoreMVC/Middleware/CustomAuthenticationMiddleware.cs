using Ajax;
using Newtonsoft.Json;

namespace BookStoreMVC.Middleware
{
    public class CustomAuthenticationMiddleware
    {
        private readonly RequestDelegate _next;

        public CustomAuthenticationMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            await _next(context);

            if (context.Response.StatusCode == 401)
            {
                context.Response.ContentType = "application/json";
                var response = JSend.Fail("User is not authenticated");
                await context.Response.WriteAsync(JsonConvert.SerializeObject(response));
            }
        }
    }
}
