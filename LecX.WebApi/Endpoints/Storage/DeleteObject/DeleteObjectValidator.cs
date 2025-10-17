﻿using FastEndpoints;
using FluentValidation;

namespace LecX.WebApi.Endpoints.Storage.DeleteObject
{
    public sealed class DeleteObjectValidator : Validator<DeleteObjectRequest>
    {
        public DeleteObjectValidator()
        {
            RuleFor(x => x.ObjectName).NotEmpty();
        }
    }
}
