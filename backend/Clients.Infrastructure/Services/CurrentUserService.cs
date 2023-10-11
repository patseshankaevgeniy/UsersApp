using Microsoft.AspNetCore.Http;
using System;
using System.Linq;
using System.Security.Claims;
using Users.Application.Common.Interfaces;

namespace Users.Infrastructure.Services;

public class CurrentUserService : ICurrentUserService
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CurrentUserService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public int UserId
    {
        get
        {
            var claim = _httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier);
            if (claim != null)
            {
                return Convert.ToInt32(claim.Value);
            }
            else { return -1; }
        }
    }
}
