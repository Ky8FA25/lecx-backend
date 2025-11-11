using LecX.Application.Abstractions.ExternalServices.Firebase;
using Microsoft.Extensions.Configuration;
using System.Net.Http.Json;

namespace LecX.Infrastructure.ExternalServices.Firebase
{
    public class FirebaseDbService : IFirebaseDbService
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl;

        public FirebaseDbService(HttpClient httpClient, IConfiguration config)
        {
            _httpClient = httpClient;
            _baseUrl = config["FirebaseSetting:DatabaseUrl"]!.TrimEnd('/');
        }

        public async Task<T?> GetAsync<T>(string path)
        {
            var url = $"{_baseUrl}/{path}.json";
            return await _httpClient.GetFromJsonAsync<T>(url);
        }

        public async Task PostAsync<T>(string path, T data)
        {
            var url = $"{_baseUrl}/{path}.json";
            await _httpClient.PostAsJsonAsync(url, data);
        }

        public async Task PutAsync<T>(string path, T data)
        {
            var url = $"{_baseUrl}/{path}.json";
            await _httpClient.PutAsJsonAsync(url, data);
        }

        public async Task DeleteAsync(string path)
        {
            var url = $"{_baseUrl}/{path}.json";
            await _httpClient.DeleteAsync(url);
        }
    }
}
