namespace MyApp.Endpoints.Abstractions.ApiErrors
{
    public interface IApiProblemDetails
    {
        public ErrorCode Code { get; }
    }
}
