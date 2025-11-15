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
            var capacity = config.GetValue<int>("Worker:QueueCapacity", 800);
            var opt = new BoundedChannelOptions(capacity) { FullMode = BoundedChannelFullMode.Wait };
            _channel = Channel.CreateBounded<(string, int)>(opt);
        }

        public async ValueTask EnqueueAsync(string studentId, int courseId, CancellationToken _ = default)
        {
            // Log để chắc chắn enqueue đã chạy
            Console.WriteLine($"[QUEUE] Enqueue {studentId}-{courseId}");
            await _channel.Writer.WriteAsync((studentId, courseId)); // <-- KHÔNG truyền ct từ HTTP
        }

        public ChannelReader<(string studentId, int courseId)> Reader => _channel.Reader;
    }
}
