namespace LecX.WebApi.Common.Dtos
{
    public abstract class AbstractResponse<T>
    {
        public T? Data { get; set; }
        public bool Success { get; set; }
        public string? Message { get; set; }
    }
}
