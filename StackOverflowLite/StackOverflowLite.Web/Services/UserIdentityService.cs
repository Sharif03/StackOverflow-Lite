using Microsoft.AspNetCore.Identity;
using StackOverflowLite.Application.Features.Posting.Services;
using StackOverflowLite.Infrastructure.Membership;
using System.Security.Claims;

namespace StackOverflowLite.Web.Services
{
    public class UserIdentityService : IUserIdentityService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserIdentityService(UserManager<ApplicationUser> userManager, IHttpContextAccessor httpContextAccessor)
        {
            _userManager = userManager;
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

        public async Task<Guid?> GetCurrentLoggedInUserGuidAsync()
        {
            string userEmail = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.Email)?.Value;

            if (userEmail != null)
            {
                ApplicationUser user = await _userManager.FindByEmailAsync(userEmail);

                if (user != null)
                {
                    return user.Id;
                }
            }

            return null;
        }
    }
}
