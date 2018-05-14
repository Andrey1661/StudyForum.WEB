using System;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using StudyForum.WebCore.ViewModels;

namespace StudyForum.WebCore.Utils
{
    public static class PrincipalExtensions
    {
        public static ClaimsIdentity AsClaimsIdentity(this IIdentity identity)
        {
            return identity as ClaimsIdentity;
        }

        public static ClaimsIdentity GetClaimsIdentity(this IPrincipal principal)
        {
            return principal?.Identity.AsClaimsIdentity();
        }

        public static Guid GetId(this IPrincipal principal)
        {
            if (principal?.Identity != null)
            {
                var idClaim = principal.GetClaimsIdentity().Claims
                    .FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);

                if (idClaim != null)
                {
                    return Guid.Parse(idClaim.Value);
                }
            }

            return Guid.Empty;
        }

        public static string GetClaim(this IPrincipal principal, string key)
        {
            if (principal?.Identity != null)
            {
                var claim = principal.GetClaimsIdentity().Claims
                    .FirstOrDefault(c => c.Type == key);

                if (claim != null)
                {
                    return claim.Value;
                }
            }

            return String.Empty;
        }

        public static UserViewModel GetUserData(this IPrincipal principal)
        {
            if (principal?.Identity != null)
            {
                var claims = principal.GetClaimsIdentity().Claims.ToDictionary(k => k.Type, v => v.Value);

                return new UserViewModel
                {
                    Id = Guid.Parse(claims["UserId"]),
                    Email = claims[ClaimTypes.Name],
                    Role = claims[ClaimTypes.Role]
                };
            }

            return null;
        }
    }
}