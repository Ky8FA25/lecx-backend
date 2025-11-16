using System.Threading.Channels;

namespace LecX.Application.Abstractions.InternalServices.Queues
{
    public interface IStudentCourseCompletionQueue
    {
        ValueTask EnqueueAsync(string studentId, int courseId);
        ChannelReader<(string studentId, int courseId)> Reader { get; }
    }
}
