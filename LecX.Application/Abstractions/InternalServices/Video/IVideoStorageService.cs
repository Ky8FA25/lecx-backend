namespace LecX.Application.Abstractions.InternalServices.Video
{
    public interface IVideoStorageService
    {
        Task<Stream> GetVideoStreamAsync(string videoId);
    }
}
