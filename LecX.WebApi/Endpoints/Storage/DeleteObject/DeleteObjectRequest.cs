﻿using FastEndpoints;

namespace LecX.WebApi.Endpoints.Storage.DeleteObject
{
    public sealed class DeleteObjectRequest
    {
        [QueryParam] public string ObjectName { get; set; } = string.Empty;
    }
}