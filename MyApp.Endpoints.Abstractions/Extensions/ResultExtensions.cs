using System.Net;
using Ardalis.Result;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using MyApp.Endpoints.Abstractions.ApiErrors;
using IResult = Microsoft.AspNetCore.Http.IResult;

namespace MyApp.Endpoints.Abstractions.Extensions
{
    public static class ResultExtensions
    {
        private const string NotFoundTitle = "Not Found";
        private const string ConflictTitle = "Conflict";
        private const string ServerErrorTitle = "Server Error";
        private const string UnauthorizedTitle = "Unauthorized";

        public static async Task<IResult> ToApiResultAsync<T>(this Task<Result<T>> result)
        {
            Result<T> awaitedResult = await result.ConfigureAwait(false);
            return awaitedResult.ToApiResult();
        }

        public static async ValueTask<IResult> ToApiResultAsync<T>(this ValueTask<Result<T>> result)
        {
            Result<T> awaitedResult = await result.ConfigureAwait(false);
            return awaitedResult.ToApiResult();
        }

        public static async ValueTask<IResult> ToApiResultAsync(this ValueTask<Result> result)
        {
            Result awaitedResult = await result.ConfigureAwait(false);
            return awaitedResult.ToApiResult();
        }

        public static IResult ToApiResult<T>(this Result<T> result)
        {
            return result.Status switch
            {
                ResultStatus.Ok => TypedResults.Ok(result.Value),
                ResultStatus.Invalid => ValidationProblem(result),
                ResultStatus.Conflict => Conflict(result),
                ResultStatus.NotFound => NotFound(result),
                ResultStatus.CriticalError => InternalServerError(result),
                ResultStatus.Error => InternalServerError(result),
                ResultStatus.NoContent => TypedResults.NoContent(),
                ResultStatus.Unauthorized => Unauthorized(result),
                _ => TypedResults.Empty
            };
        }

        public static async ValueTask<IResult> ToApiResultAsync<T>(this ValueTask<Result<T>> result, string contentType,
            string fileName)
        {
            Result<T> awaitedResult = await result.ConfigureAwait(false);
            return awaitedResult.ToApiResult(contentType, fileName);
        }

        public static IResult ToApiResult<T>(this Result<T> result, string contentType, string fileName)
        {
            ArgumentNullException.ThrowIfNull(result);
            ArgumentException.ThrowIfNullOrWhiteSpace(contentType);
            ArgumentException.ThrowIfNullOrWhiteSpace(fileName);

            if (!result.IsSuccess)
            {
                return result.ToApiResult();
            }

            return result.Value switch
            {
                byte[] bytes => TypedResults.Bytes(bytes, contentType, fileName),
                ReadOnlyMemory<byte> memory => TypedResults.Bytes(memory, contentType, fileName),
                _ => result.ToApiResult()
            };
        }

        private static NotFound<ApiProblemDetails> NotFound<T>(Result<T> result)
        {
            ApiProblemDetails problemDetail = new(ErrorCode.NotFound, HttpStatusCode.NotFound, NotFoundTitle,
                result.Errors.FirstOrDefault());
            return TypedResults.NotFound(problemDetail);
        }

        private static BadRequest<InputValidationProblemDetails> ValidationProblem<T>(Result<T> result)
        {
            InputValidationProblemDetails problemDetail = new(result.ValidationErrors);
            return TypedResults.BadRequest(problemDetail);
        }

        private static Conflict<ApiProblemDetails> Conflict<T>(Result<T> result)
        {
            ApiProblemDetails problemDetail = new(ErrorCode.Conflict, HttpStatusCode.Conflict,
                ConflictTitle, result.Errors.FirstOrDefault());

            return TypedResults.Conflict(problemDetail);
        }

        private static InternalServerError<ApiProblemDetails> InternalServerError<T>(Result<T> result)
        {
            ApiProblemDetails problemDetail = new(ErrorCode.ServerError, HttpStatusCode.InternalServerError,
                ServerErrorTitle, result.Errors.FirstOrDefault());
            return TypedResults.InternalServerError(problemDetail);
        }

        private static ProblemHttpResult Unauthorized<T>(Result<T> result)
        {
            ApiProblemDetails problemDetail = new(ErrorCode.Unauthorized, HttpStatusCode.Unauthorized,
                UnauthorizedTitle, result.Errors.FirstOrDefault());
            return TypedResults.Problem(problemDetail);
        }
    }
}
