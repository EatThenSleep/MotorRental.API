using System.Security.Claims;

namespace MotorRental.Infrastructure.Presentation.Extension
{
    public static class HttpContextExt
    {
        public static string GetUserId(this HttpContext context)
        {
            var identity = context.User.Identity as ClaimsIdentity;
            IEnumerable<Claim> claims = identity.Claims;
            return identity.FindFirst(c => c.Type == "UserId").Value;
        }

        public static string GetRole(this HttpContext context)
        {
            var identity = context.User.Identity as ClaimsIdentity;
            IEnumerable<Claim> claims = identity.Claims;
            return identity.FindFirst(c => c.Type == ClaimTypes.Role).Value;
        }
    }
}
