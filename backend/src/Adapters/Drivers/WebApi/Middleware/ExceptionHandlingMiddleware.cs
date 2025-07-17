using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Text.Json;
using TPCadastroUsuario.Core.Exceptions;

namespace TPCadastroUsuario.Adapters.Drivers.WebApi.Middleware;

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
        catch (DomainException dex)
        {
            _logger.LogWarning(dex, "Erro de dóminio");
            await HandleProblemAsync(context, HttpStatusCode.BadRequest, dex.Message);
        }
        catch (ValidationException vex)
        {
            _logger.LogWarning(vex, "Validation failure");
            var errors = vex.Errors
                .GroupBy(e => e.PropertyName)
                .ToDictionary(
                    g => g.Key,
                    g => g.Select(e => e.ErrorMessage).ToArray()
                );
            await HandleProblemAsync(context, (HttpStatusCode)422, "Validation errors", errors);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unhandled exception");
            await HandleProblemAsync(context, HttpStatusCode.InternalServerError, "Ocorreu um erro interno no servidor.");
        }
    }
    private static async Task HandleProblemAsync(HttpContext contexto, HttpStatusCode codigohttp, string titulo, object? detalhes = null)
    {
        contexto.Response.ContentType = "application/problem+json";
        contexto.Response.StatusCode = (int)codigohttp;

        var colecaoProblemas = new ProblemDetails
        {
            Status = (int)codigohttp,
            Title = titulo,
            Detail = detalhes is string ? (string)detalhes : null
        };

        if (detalhes is not string && detalhes is object)
        {
            var jsonOpcoes = new JsonSerializerOptions(JsonSerializerDefaults.Web);
            var dictionaryAnonimo = new
            {
                colecaoProblemas.Status,
                colecaoProblemas.Title,
                errors = detalhes
            };
            await contexto.Response.WriteAsJsonAsync(dictionaryAnonimo, jsonOpcoes);
        }
        else
        {
            await contexto.Response.WriteAsJsonAsync(colecaoProblemas);
        }
    }

}
