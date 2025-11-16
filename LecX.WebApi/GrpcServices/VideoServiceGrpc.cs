using LecX.WebApi.Grpc;
using MediatR;
using Grpc.Core;
using Google.Protobuf;
using LecX.Application.Features.Video.StreamVideo;

namespace LecX.WebApi.GrpcServices
{
    public class VideoServiceGrpc : VideoService.VideoServiceBase
    {
        private readonly ISender _mediator;

        public VideoServiceGrpc(ISender mediator)
        {
            _mediator = mediator;
        }

        public override async Task StreamVideo(
            VideoRequest request,
            IServerStreamWriter<VideoChunk> responseStream,
            ServerCallContext context)
        {
            var result = await _mediator.Send(new StreamVideoQuery(request.VideoId));

            int seq = 0;
            foreach (var chunk in result.Chunks)
            {
                await responseStream.WriteAsync(new VideoChunk
                {
                    Content = ByteString.CopyFrom(chunk),
                    Sequence = seq++
                });
            }
        }
    }
}