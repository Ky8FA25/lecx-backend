namespace LecX.Application.Common.Dtos
{
    public abstract class AbstractResponse
    {
        public bool Success { get; set; } = false;
        public string? Message { get; set; }
    }
}
