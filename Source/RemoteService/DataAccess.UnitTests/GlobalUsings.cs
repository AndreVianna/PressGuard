global using System.Extensions;
global using System.Globalization;
global using System.Results;
global using System.Security.Cryptography;
global using System.Text;

global using FluentAssertions;

global using Microsoft.Extensions.Configuration;
global using Microsoft.Extensions.DependencyInjection;
global using Microsoft.Extensions.Logging.Abstractions;

global using NSubstitute;
global using NSubstitute.ExceptionExtensions;

global using RemoteService.Authentication;
global using RemoteService.Handlers.Auth;
global using RemoteService.Identity;
global using RemoteService.Models;
global using RemoteService.Repositories.Auth;

global using Xunit;

global using static System.Text.Json.JsonSerializer;
