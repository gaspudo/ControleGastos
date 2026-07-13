using System.Net;
using System.Text.Json;
using ControleGastos.Domain.Exceptions;

namespace ControleGastos.Api.Middlewares;

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;

    public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
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
        catch (Exception ex)
        {
            _logger.LogError(ex, "Uma exceção não tratada ocorreu durante a requisição.");
            await HandleExceptionAsync(context, ex);
        }
    }

    private static async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/json";

        // mapear cada excecao criada baseado no tipo da exceção
        context.Response.StatusCode = exception switch
        {
            AppException appEx => appEx.StatusCode,
            _ => (int)HttpStatusCode.InternalServerError
        };

        var response = new ErrorResponse(
            StatusCode: context.Response.StatusCode,
            Message: context.Response.StatusCode == (int)HttpStatusCode.InternalServerError 
                ? "Ocorreu um erro interno no servidor." 
                : exception.Message
        );

        var jsonOptions = new JsonSerializerOptions 
        { 
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase 
        };

        await context.Response.WriteAsync(JsonSerializer.Serialize(response, jsonOptions));
    }
}

// Record simples para padronizar a resposta de erro enviada ao cliente
public record ErrorResponse(int StatusCode, string Message);