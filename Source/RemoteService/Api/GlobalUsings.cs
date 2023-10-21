// Global using directives

global using System.ComponentModel.DataAnnotations;
global using System.Diagnostics.CodeAnalysis;
global using System.Extensions;
global using System.IdentityModel.Tokens.Jwt;
global using System.Net;
global using System.Results;
global using System.Security.Claims;
global using System.Security.Cryptography;
global using System.Security.Principal;
global using System.Text;
global using System.Text.Encodings.Web;
global using System.Utilities;

global using Microsoft.AspNetCore.Authentication;
global using Microsoft.AspNetCore.Authorization;
global using Microsoft.AspNetCore.Mvc;
global using Microsoft.AspNetCore.Mvc.Filters;
global using Microsoft.AspNetCore.Mvc.ModelBinding;
global using Microsoft.Extensions.Options;
global using Microsoft.IdentityModel.Tokens;
global using Microsoft.OpenApi.Models;

global using RemoteService.Authentication;
global using RemoteService.Handlers.Auth;
global using RemoteService.Extensions;
global using RemoteService.Identity;
global using RemoteService.Utilities;

global using Serilog;

global using Swashbuckle.AspNetCore.Annotations;
global using Swashbuckle.AspNetCore.SwaggerGen;

global using ILogger = Microsoft.Extensions.Logging.ILogger;
