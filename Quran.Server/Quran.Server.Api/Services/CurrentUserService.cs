using Microsoft.AspNetCore.Http;

using Quran.Server.Application.Common.Interfaces;

using System;
using System.Security.Claims;

namespace Quran.Server.Api.Services
{
    public class CurrentUserService : ICurrentUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public Guid? UserId
        {
            get
            {
                var userId = _httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);
                return Guid.TryParse(userId, out Guid userGuid) ? userGuid : (Guid?)null;
            }
        }
    }
}
