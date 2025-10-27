﻿using LecX.Application.Abstractions.ExternalServices.GoogleAuth;
using LecX.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace LecX.Infrastructure.ExternalServices.GoogleAuth;

public class GoogleAuthService : IGoogleAuthService
{
    private readonly SignInManager<User> _signInManager;
    private readonly UserManager<User> _userManager;

    public GoogleAuthService(
        SignInManager<User> signInManager,
        UserManager<User> userManager)
    {
        _signInManager = signInManager;
        _userManager = userManager;
    }

    public async Task<ExternalLoginInfo?> GetExternalLoginInfoAsync()
    {
        return await _signInManager.GetExternalLoginInfoAsync();
    }

    public async Task<SignInResult> ExternalLoginSignInAsync(string provider, string providerKey)
    {
        return await _signInManager.ExternalLoginSignInAsync(provider, providerKey, isPersistent: false);
    }

    public async Task<User> AutoProvisionUserAsync(ExternalLoginInfo info)
    {
        var email = info.Principal.FindFirstValue(ClaimTypes.Email);
        var name = info.Principal.FindFirstValue(ClaimTypes.Name);
        var avatar = info.Principal.FindFirstValue("urn:google:picture");

        var parts = name?.Trim().Split(' ', StringSplitOptions.RemoveEmptyEntries);
        var user = new User
        {
            UserName = email,
            Email = email,
            FirstName = parts?.FirstOrDefault() ?? "",
            LastName = parts?.Length > 1 ? string.Join(" ", parts.Skip(1)) : "",
            ProfileImagePath = avatar,
            EmailConfirmed = true
        };

        var result = await _userManager.CreateAsync(user);
        if (!result.Succeeded)
            throw new InvalidOperationException(result.Errors.First().Description);

        return user;
    }

    public async Task<IdentityResult> AddLoginAsync(User user, ExternalLoginInfo info)
    {
        return await _userManager.AddLoginAsync(user, info);
    }

    public async Task SignInAsync(User user)
    {
        await _signInManager.SignInAsync(user, isPersistent: false);
    }
}



