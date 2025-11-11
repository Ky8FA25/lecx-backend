namespace LecX.Application.Abstractions.ExternalServices.Firebase
{
    public interface IFirebaseDbService
    {
        Task<T?> GetAsync<T>(string path);
        Task PostAsync<T>(string path, T data);
        Task PutAsync<T>(string path, T data);
        Task DeleteAsync(string path);
    }
}
