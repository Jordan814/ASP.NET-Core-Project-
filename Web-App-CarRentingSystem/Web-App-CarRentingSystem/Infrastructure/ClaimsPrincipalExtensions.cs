using System.Security.Claims;
using static Web_App_CarRentingSystem.WebConstants;

namespace Web_App_CarRentingSystem.Infrastructure
{
    public static class ClaimsPrincipalExtensions
    {

        public static string GetId(this ClaimsPrincipal user)
            => user.FindFirst(ClaimTypes.NameIdentifier).Value;

        public static bool IsAdmin(this ClaimsPrincipal user)
            => user.IsInRole(WebConstants.AdministratorRoleName);
    }
}
