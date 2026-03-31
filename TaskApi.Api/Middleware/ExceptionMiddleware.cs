using System.Net;
using System.Text.Json;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionMiddleware> _logger;

    public ExceptionMiddleware(RequestDelegate next,ILogger<ExceptionMiddleware> logger)
    {
        _next = next;
        _logger = logger; 
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);    
        }
        catch(Exception ex)
        {
            _logger.LogError(ex,"Unhandled Exception Error Occured");
            
            var response = new ApiResponse<string>
            {
              Success = false,
              Message = "Something went wrong",
              Data = ex.Message 
            };
            
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = 500;

            await context.Response.WriteAsJsonAsync(response);
        }
    }
}