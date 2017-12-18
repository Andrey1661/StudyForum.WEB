using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using StudyForum.DataAccess.Core.Models;

namespace StudyForum.WEB
{
    public class SignInManager : ISignInManager
    {
        private IAuthenticationManager Manager { get; }

        public SignInManager(IAuthenticationManager manager)
        {
            Manager = manager;
        }

        public void SignIn(UserModel user, bool isPersistent)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Email),
                new Claim(ClaimTypes.Role, user.Role.Name)
            };

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationType);
            Manager.SignIn(new AuthenticationProperties {IsPersistent = isPersistent}, identity);
        }

        public void SignOut()
        {
            Manager.SignOut(CookieAuthenticationDefaults.AuthenticationType);
        }
    }
}