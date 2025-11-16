using MediatR;

namespace LecX.Application.Features.Video.StreamVideo
{
    public record StreamVideoQuery(string VideoId) : IRequest<StreamVideoResult>;
}
