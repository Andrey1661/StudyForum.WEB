using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Web;

namespace StudyForum.WEB.Utils
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
    }
}