namespace LecX.WebApi.Endpoints.Auth.Register
{
    public sealed class RegisterResponse
    {
        public string Message { get; set; }
        public string? ConfirmUrl { get; set; }
    }
}