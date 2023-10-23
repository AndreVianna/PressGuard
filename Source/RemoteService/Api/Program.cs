using RemoteService.Communication;

var builder = WebApplication.CreateBuilder(args);

var env = builder.Environment;
builder.Configuration
       .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
       .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true, reloadOnChange: true);

if (env.IsDevelopment())
    builder.Configuration.AddUserSecrets<Program>();

Log.Logger = new LoggerConfiguration()
            .ReadFrom.Configuration(builder.Configuration)
            .CreateLogger();

builder.Host.UseSerilog();

const string authScheme = "Bearer";
builder.Services.AddAuthentication(options => {
    options.DefaultAuthenticateScheme = authScheme;
    options.DefaultChallengeScheme = authScheme;
}).AddScheme<AuthenticationSchemeOptions, ApiKeyAuthenticationHandler>(authScheme, null);

builder.Services.AddApiVersioning(options => {
    options.ReportApiVersions = true;
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.DefaultApiVersion = new(1, 0);
});
builder.Services.AddVersionedApiExplorer(options => {
    options.GroupNameFormat = "'v'VVV";
    options.SubstituteApiVersionInUrl = true;
});

builder.Services.AddSystemUtilities();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddScoped<IUserAccessor, ApiUserAccessor>();
builder.Services.AddScoped<IHasher, Hasher>();
builder.Services.AddScoped<IEmailSender, EmailSender>();
builder.Services.AddDomainHandlers<TokenGenerator>(builder.Configuration);
builder.Services.AddRepositories();
builder.Services.AddScoped(sp => new CustomExceptionFilter(sp.GetRequiredService<ILoggerFactory>(), env));

builder.Services.AddControllers(options => options.Filters.Add<CustomExceptionFilter>())
    .ConfigureApiBehaviorOptions(options => options.SuppressMapClientErrors = true);

builder.Services.AddEndpointsApiExplorer();
const string apiTitle = "RemoteService.API";
const string apiVersion = "v1";
const string authHeaderKey = "Authorization";
builder.Services.AddSwaggerGen(c => {
    c.SwaggerDoc(apiVersion, new() { Title = apiTitle, Version = apiVersion });

    c.OperationFilter<AuthResponsesOperationFilter>();
    c.AddSecurityDefinition(authScheme, new() {
        Description = "Please enter a valid JWT token.",
        Reference = new() {
            Type = ReferenceType.SecurityScheme,
            Id = authScheme,
        },
        Name = authHeaderKey,
        In = ParameterLocation.Header,
        Scheme = authScheme,
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
    });

    c.DocInclusionPredicate((_, _) => true);
    c.TagActionsBy(api => new[] { api.GroupName });
    c.EnableAnnotations();
});

var app = builder.Build();

if (app.Environment.IsDevelopment()) {
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
