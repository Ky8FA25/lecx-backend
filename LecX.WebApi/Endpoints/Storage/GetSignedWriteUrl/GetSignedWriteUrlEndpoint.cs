﻿using FastEndpoints;
using LecX.Application.Abstractions.ExternalServices.GoogleStorage;

namespace LecX.WebApi.Endpoints.Storage.GetSignedWriteUrl
{
    public sealed class GetSignedWriteUrlEndpoint(IGoogleStorageService storage)
       : Endpoint<GetSignedWriteUrlRequest, GetSignedWriteUrlResponse>
    {
        public override void Configure()
        {
            Get("/api/storage/signed-write");
            // Authorize();
            Summary(s =>
            {
                s.Summary = "Lấy URL ghi có chữ ký (PUT)";
                s.Description = "Client upload trực tiếp lên GCS qua PUT, phải gửi đúng Content-Type khi PUT.";
            });
        }

        public override Task HandleAsync(GetSignedWriteUrlRequest req, CancellationToken ct)
        {
            var url = storage.GetSignedWriteUrl(
                req.ObjectName,
                req.ContentType,
                TimeSpan.FromSeconds(req.TtlSeconds));

            return SendOkAsync(new GetSignedWriteUrlResponse { Success = true, Url = url }, ct);
        }
    }
}
