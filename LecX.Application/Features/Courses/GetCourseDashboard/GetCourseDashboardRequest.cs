using MediatR;

namespace LecX.Application.Features.Courses.GetCourseDashboard
{
    public sealed record GetCourseDashboardRequest(int CourseId) : IRequest<GetCourseDashboardResponse>;
}

