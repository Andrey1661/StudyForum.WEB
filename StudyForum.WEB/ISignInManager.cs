using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using StudyForum.DataAccess.Core.Models;

namespace StudyForum.WEB
{
    public interface ISignInManager
    {
        void SignIn(UserModel user);
        void SignOut();
    }
}