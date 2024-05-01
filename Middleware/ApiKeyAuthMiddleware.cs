namespace WizardFormBackend.Middleware
{
    public class ApiKeyAuthMiddleware(RequestDelegate next, IConfiguration configuration)
    {
        private readonly RequestDelegate _next = next;
        private readonly IConfiguration _configuration = configuration;

        private const string API_KEY_HEADER = "api-key";
        private const string API_KEY_SECTION = "Authentication:ApiKey";

        public async Task InvokeAsync(HttpContext context)
        {
            var providedKey = context.Request.Headers[API_KEY_HEADER].FirstOrDefault();
            if(!IsValidKey(providedKey))
            {
                await GenerateResponse(context, 401, "Invalid API Key");
                return;
            }
            
            await _next(context);
        }


        private bool IsValidKey(string? providedKey)
        {
            
            if(string.IsNullOrEmpty(providedKey)) return false;
            var apiKey = _configuration.GetValue<string>(API_KEY_SECTION);
            return string.Equals(apiKey, providedKey, StringComparison.Ordinal);
        }

        private static async Task GenerateResponse(HttpContext context, int statusCode, string message)
        {
            context.Response.StatusCode = statusCode;
            await context.Response.WriteAsync(message);
        }
    }
}
