using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.Owin.Security.OAuth;
using StudyForum.DataAccess.Core.Abstract.Services;

namespace StudyForum.WEB.Providers
{
    public class ApplicationServerProvider : OAuthAuthorizationServerProvider
    {
        public override Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated();
            return Task.FromResult(0);
        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            //var user = await UserService.GetUserAsync(context.UserName, context.Password);

            //if (user != null)
            //{
            //    context.Validated();
            //    return;
            //}

            //context.SetError("Ошибка аутентификации", "Пользователь отсутствует в системе");
        }
    }
}