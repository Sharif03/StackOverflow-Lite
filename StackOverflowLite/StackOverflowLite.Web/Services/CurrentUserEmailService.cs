using StackOverflowLite.Application.Features.Posting.Services;
using System.Security.Claims;

namespace StackOverflowLite.Web.Services
{
    public class CurrentUserEmailService : ICurrentUserEmailService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CurrentUserEmailService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<string> GetCurrentLoggedInUserEmailAsync()
        {
            // Retrieve the claims principal representing the currently authenticated user
            ClaimsPrincipal user = _httpContextAccessor.HttpContext.User;

            // Retrieve the email claim
            Claim emailClaim = user.FindFirst(ClaimTypes.Email);

            if (emailClaim != null)
            {
                // Get the email value
                return emailClaim.Value;
            }
            else
            {
                // Email claim not found
                return null;
            }
        }
    }
}
