using FastEndpoints;
using LecX.Application.Abstractions.ExternalServices.GoogleStorage;

namespace LecX.WebApi.Endpoints.Storage.DeleteObject
{
    public sealed class DeleteObjectEndpoint(IGoogleStorageService storage)
      : Endpoint<DeleteObjectRequest>
    {
        public override void Configure()
        {
            Delete("/api/storage/delete-object");
            Roles("Admin");
            Summary(s =>
            {
                s.Summary = "Xoá object khỏi GCS (Nhìn z chứ k xóa được)";
                s.Description = "Idempotent: nếu không tồn tại thì cũng coi như thành công (204).";
            });
        }

        public override async Task HandleAsync(DeleteObjectRequest req, CancellationToken ct)
        {
            try
            {
                await storage.DeleteAsync(req.ObjectName, ct);
                await SendAsync( new { message = "sucess"}, StatusCodes.Status200OK, ct);
            }
            catch (Google.GoogleApiException gae)
            {
                // ví dụ quyền không đủ, bucket sai, v.v.
                await SendAsync(new { error = gae.Message }, 500); // ✅ chỉ string
            }
            catch (Exception ex)
            {
                await SendAsync(new { error = ex }, 500); // ✅ chỉ string
            }
        }
    }
}
