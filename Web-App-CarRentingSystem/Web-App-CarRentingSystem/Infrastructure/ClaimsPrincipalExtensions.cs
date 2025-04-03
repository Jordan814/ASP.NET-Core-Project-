using System.Security.Claims;
using Web_App_CarRentingSystem.Areas.Admin;
using static Web_App_CarRentingSystem.Areas.Admin.AdminConstants;


namespace Web_App_CarRentingSystem.Infrastructure
{
    public static class ClaimsPrincipalExtensions
    {

        public static string GetId(this ClaimsPrincipal user)
            => user.FindFirst(ClaimTypes.NameIdentifier).Value;

        public static bool IsAdmin(this ClaimsPrincipal user)
            => user.IsInRole(AdminConstants.AdministratorRoleName);
    }
}
