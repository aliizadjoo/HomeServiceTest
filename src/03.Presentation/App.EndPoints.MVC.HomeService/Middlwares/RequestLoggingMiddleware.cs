namespace App.EndPoints.MVC.HomeService.Middlwares
{
    public class RequestLoggingMiddleware (ILogger<RequestLoggingMiddleware> _logger , RequestDelegate _next)
    {
        public async Task InvokeAsync(HttpContext context ) 
        {
            try
            {
               await _next(context);
            }
            catch (Exception ex) 
            {
                _logger.LogError(ex, "یک خطا در سیستم رخ داد: {Message}", ex.Message);

                var errorMessage = Uri.EscapeDataString("متاسفانه خطایی رخ داده است");

                context.Response.Redirect($"/Home/AccessDenied?message={errorMessage}");

            }
        
        }
    }
}
