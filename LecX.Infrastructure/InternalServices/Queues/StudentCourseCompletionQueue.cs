using LecX.Application.Abstractions.InternalServices.Queues;
using Microsoft.Extensions.Configuration;
using System.Threading.Channels;

namespace LecX.Infrastructure.InternalServices.Queues
{
    public sealed class StudentCourseCompletionQueue : IStudentCourseCompletionQueue
    {
        private readonly Channel<(string studentId, int courseId)> _channel;

        public StudentCourseCompletionQueue(IConfiguration config)
        {
            var capacity = config.GetValue<int>("Worker:QueueCapacity", 1000);

            _channel = Channel.CreateBounded<(string, int)>(
                new BoundedChannelOptions(capacity)
                {
                    FullMode = BoundedChannelFullMode.Wait,
                    SingleReader = false,
                    SingleWriter = false
                });
        }

        public async ValueTask EnqueueAsync(string studentId, int courseId)
        {
            await _channel.Writer.WriteAsync((studentId, courseId));
        }

        public ChannelReader<(string studentId, int courseId)> Reader => _channel.Reader;
    }
}
