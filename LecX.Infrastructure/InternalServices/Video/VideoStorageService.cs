using Google.Cloud.Storage.V1;
using LecX.Application.Abstractions.InternalServices.Video;
using Microsoft.Extensions.Configuration;
using Org.BouncyCastle.Asn1.Ocsp;

namespace LecX.Infrastructure.Video
{
    public class GcsVideoStorageService : IVideoStorageService
    {
        private readonly StorageClient _client;
        private readonly string _bucketName;

        public GcsVideoStorageService(StorageClient client, IConfiguration config)
        {
            _client = client;
            _bucketName = config["GcpStorage:Bucket"];
        }

        public async Task<Stream> GetVideoStreamAsync(string videoId)
        {
            var ms = new MemoryStream();
            string objectName = Uri.UnescapeDataString(videoId);
            await _client.DownloadObjectAsync(_bucketName, objectName, ms);
            ms.Position = 0;
            return ms;
        }
    }
}
