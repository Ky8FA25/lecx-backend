using LecX.Application.Common.Dtos;
using Microsoft.AspNetCore.OData;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OData.ModelBuilder;

namespace LecX.Infrastructure.Extensions.OData;

public static class ODataMinimal
{
    public static IMvcBuilder AddODataSupport(this IServiceCollection services)
    {
        var builder = new ODataConventionModelBuilder();

        var user = builder.EntitySet<UserODataDto>("UserOData").EntityType;
        user.HasKey(u => u.Id);

        var course = builder.EntitySet<CourseODataDto>("CourseOData").EntityType;
        course.HasKey(u => u.CourseId);

        var category = builder.EntitySet<CategoryODataDto>("CategoryOData").EntityType;
        category.HasKey(u => u.CategoryId);

        var studentCourse = builder.EntitySet<StudentCourseODataDto>("StudentCourseOData").EntityType;
        studentCourse.HasKey(u => u.StudentCourseId);

        var payment = builder.EntitySet<PaymentODataDto>("PaymentOData").EntityType;
        payment.HasKey(u => u.PaymentId);

        var report = builder.EntitySet<ReportODataDto>("ReportOData").EntityType;
        report.HasKey(u => u.ReportId);

        var edm = builder.GetEdmModel();
        return services.AddControllers().AddOData(opt =>
            opt.AddRouteComponents("odata", edm)
               .Select()
               .Filter()
               .OrderBy()
               .Expand()
               .Count()
               .SetMaxTop(100));
    }
}
