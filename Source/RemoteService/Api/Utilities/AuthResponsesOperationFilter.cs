namespace RemoteService.Utilities;

[ExcludeFromCodeCoverage]
public class AuthResponsesOperationFilter : IOperationFilter {
    private const string _authScheme = "Bearer";
    private const string _authHeaderKey = "Authorization";

    public void Apply(OpenApiOperation operation, OperationFilterContext context) {
        var parentRequiresAuthorization = context.MethodInfo.DeclaringType!.GetCustomAttributes(true).OfType<AuthorizeAttribute>().Any();
        var actionRequiresAuthorization = context.MethodInfo.GetCustomAttributes(true).OfType<AuthorizeAttribute>().Any();
        var actionAllowAnonymous = context.MethodInfo.GetCustomAttributes(true).OfType<AllowAnonymousAttribute>().Any();

        var showLock = actionRequiresAuthorization || (parentRequiresAuthorization && !actionAllowAnonymous);

        if (!showLock) return;
        var securityRequirement = new OpenApiSecurityRequirement { {
            new OpenApiSecurityScheme {
                Description = "Please enter a valid JWT token.",
                Reference = new() {
                    Type = ReferenceType.SecurityScheme,
                    Id = _authScheme,
                },
                Name = _authHeaderKey,
                In = ParameterLocation.Header,
                Scheme = _authScheme,
                Type = SecuritySchemeType.Http,
                BearerFormat = "JWT",
            },
            new List<string>()
        }};
        operation.Security = new List<OpenApiSecurityRequirement> { securityRequirement };
        operation.Responses.Add("401", new() { Description = "Unauthorized" });
    }
}
