using ECommerce.Application.Exceptions;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.API.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (OperationCanceledException) when (context.RequestAborted.IsCancellationRequested)
            {
                throw;
            }
            catch (Exception exception)
            {
                if (context.Response.HasStarted || context.RequestAborted.IsCancellationRequested)
                {
                    throw;
                }

                ProblemDetails problem;
                if (exception is ValidationException validationException)
                {
                    var errors = validationException.Errors
                        .GroupBy(error => error.PropertyName)
                        .ToDictionary(group => group.Key,
                            group => group.Select(error => error.ErrorMessage).Distinct().ToArray());

                    problem = new ValidationProblemDetails(errors)
                    {
                        Status = StatusCodes.Status400BadRequest,
                        Title = "One or more validation errors occurred."
                    };
                }
                else if (exception is BusinessRuleException)
                {
                    problem = new ProblemDetails
                    {
                        Status = StatusCodes.Status400BadRequest,
                        Title = "Business rule violation.",
                        Detail = exception.Message
                    };
                }
                else
                {
                    problem = new ProblemDetails
                    {
                        Status = StatusCodes.Status500InternalServerError,
                        Title = "An unexpected error occurred."
                    };
                }

                context.Response.Clear();
                context.Response.StatusCode = problem.Status!.Value;
                await context.Response.WriteAsJsonAsync<object>(
                    problem,
                    options: null,
                    contentType: "application/problem+json",
                    cancellationToken: context.RequestAborted);
            }
        }
    }
}
