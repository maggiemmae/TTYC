using Microsoft.AspNetCore.Http;
using TTYC.Application.Interfaces;

namespace TTYC.Application.Services
{
    public class CurrentUserService : ICurrentUserService
    {
        private readonly IHttpContextAccessor httpContextAccessor;

        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            this.httpContextAccessor = httpContextAccessor;
        }

        public Guid UserId =>
            Guid.Parse(this.httpContextAccessor.HttpContext?.User?.FindFirst("sub").Value ??
                       0.ToString());
    }
}
