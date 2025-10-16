using FastEndpoints;
using LecX.Application.Abstractions.ExternalServices.GoogleStorage;
using LecX.Application.Common.Utils;
using LecX.Application.Commons.Constants;

namespace LecX.WebApi.Endpoints.Storage.Upload
{
    public sealed class UploadEndpoint(IGoogleStorageService storage)
       : Endpoint<UploadRequest, UploadResponse>
    {
        public override void Configure()
        {
            Post("/api/storage/upload");
            // Authorize(); // bật nếu cần
            AllowFileUploads();
            Summary(s =>
            {
                s.Summary = "Upload file lên GCS qua server";
                s.Description = "Nhận multipart file, upload bằng server và trả về objectName + public URL.";
            });
        }

        public override async Task HandleAsync(UploadRequest req, CancellationToken ct)
        {
            var file = req.File;
            var contentType = string.IsNullOrWhiteSpace(file.ContentType)
                ? "application/octet-stream"
                : file.ContentType;

            await using var stream = file.OpenReadStream();

            var (ext, fileName) = FileNameHelper.Normalize(file.FileName);

            var folder = string.IsNullOrWhiteSpace(req.Prefix)
                ? StoragePaths.Default
                : req.Prefix.Trim().Trim('/');

            // thêm Guid phía sau để tránh trùng tên
            var objectName = $"{folder}/{fileName}-{Guid.NewGuid():N}{ext}";

            var savedName = await storage.UploadAsync(stream, objectName, contentType, ct);
            var publicUrl = storage.GetPublicUrl(savedName);

            await SendOkAsync(new UploadResponse
            {
                Success = true,
                ObjectName = savedName,
                PublicUrl = publicUrl
            }, ct);
        }
    }
}
