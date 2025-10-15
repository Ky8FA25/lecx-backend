namespace LecX.Application.Common.Dtos
{
    public record ResponseRecord<T>(
        bool Success,
        string Message,
        T? Data = default
    )
    {
        public static ResponseRecord<T> Ok(T data, string message = "Success")
            => new(true, message, data);

        public static ResponseRecord<T> Fail(string message)
            => new(false, message);
    }
}
