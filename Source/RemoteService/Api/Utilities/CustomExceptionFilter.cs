namespace RemoteService.Utilities;

[ExcludeFromCodeCoverage]
internal class CustomExceptionFilter : IExceptionFilter {
    private readonly IWebHostEnvironment _env;
    private readonly ILogger _logger;
    private const string _errorMessage = "An unhandled exception occurred.";

    public CustomExceptionFilter(ILoggerFactory loggerFactory, IWebHostEnvironment env) {
        _env = env;
        _logger = loggerFactory.CreateLogger<CustomExceptionFilter>();
    }

    public void OnException(ExceptionContext context) {
        var exception = context.Exception;
        _logger.LogError(exception, _errorMessage);

        context.Result = new ObjectResult(new ProblemDetails {
            Title = _env.IsDevelopment() ? _errorMessage : exception.Message,
            Detail = _env.IsDevelopment() ? exception.ToString() : exception.Message,
            Status = (int)HttpStatusCode.InternalServerError,
            Instance = context.HttpContext.Request.Path,
            Type = $"https://httpstatuses.com/{(int)HttpStatusCode.InternalServerError}",
        });
        context.ExceptionHandled = true;
    }
}