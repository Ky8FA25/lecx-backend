using AutoMapper;
using LecX.Application.Abstractions.Persistence;
using LecX.Application.Common.Dtos;
using LecX.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.EntityFrameworkCore;

namespace LecX.WebApi.ODataControllers
{
    [Authorize(Roles = "Admin")]
    public class UserODataController : ODataController
    {
        private readonly IAppDbContext _db;
        private readonly IMapper _mapper;

        public UserODataController(IAppDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        //[ApiExplorerSettings(IgnoreApi = true)]
        [EnableQuery(PageSize = 50)]
        public async Task<List<UserODataDto>> Get()
        {
            // 1️⃣ Lấy tất cả users
            var users = await _db.Set<User>().ToListAsync();
            var userIds = users.Select(u => u.Id).ToList();

            // 2️⃣ Lấy tất cả user roles + role name (join trực tiếp trên DbContext)
            var userRoles = await _db.Set<IdentityUserRole<string>>()
                .Where(ur => userIds.Contains(ur.UserId))
                .Join(
                    _db.Set<IdentityRole>(),
                    ur => ur.RoleId,
                    r => r.Id,
                    (ur, r) => new { ur.UserId, RoleName = r.Name }
                )
                .ToListAsync();

            // 3️⃣ Tạo dictionary UserId -> RoleName (lấy role đầu tiên nếu nhiều)
            var roleDict = userRoles
                .GroupBy(ur => ur.UserId)
                .ToDictionary(
                    g => g.Key,
                    g => g.Select(x => x.RoleName).FirstOrDefault() ?? "Unknown"
                );

            // 4️⃣ Mapping DTO + gán role
            var dtos = users.Select(u =>
            {
                var dto = _mapper.Map<UserODataDto>(u);
                dto.Role = roleDict.TryGetValue(u.Id, out var role) ? role : "Unknown";
                return dto;
            }).ToList();

            return dtos;
        }
    }
}
