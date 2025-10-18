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
                s.Summary = "Xoá object khỏi GCS";
                s.Description = "Idempotent: nếu không tồn tại thì cũng coi như thành công (204).";
            });
        }

        public override async Task HandleAsync(DeleteObjectRequest req, CancellationToken ct)
        {
            await storage.DeleteAsync(req.ObjectName, ct);
            await SendNoContentAsync(ct);
        }
    }
}
