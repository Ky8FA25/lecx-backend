using LecX.Application.Abstractions.InternalServices.Video;
using MediatR;

namespace LecX.Application.Features.Video.StreamVideo
{
    public class StreamVideoHandler : IRequestHandler<StreamVideoQuery, StreamVideoResult>
    {
        private readonly IVideoStorageService _videoStorage;

        public StreamVideoHandler(IVideoStorageService videoStorage)
        {
            _videoStorage = videoStorage;
        }

        public async Task<StreamVideoResult> Handle(StreamVideoQuery request, CancellationToken cancellationToken)
        {
            var stream = await _videoStorage.GetVideoStreamAsync(request.VideoId);

            const int chunkSize = 1024 * 1024; // 1MB
            byte[] buffer = new byte[chunkSize];

            var result = new StreamVideoResult();
            int bytesRead;

            while ((bytesRead = await stream.ReadAsync(buffer, 0, chunkSize, cancellationToken)) > 0)
            {
                var actualChunk = new byte[bytesRead];
                Array.Copy(buffer, actualChunk, bytesRead);

                result.Chunks.Add(actualChunk);
            }

            return result;
        }
    }
}