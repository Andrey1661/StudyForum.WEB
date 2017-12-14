using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudyForum.WEB.ViewModels
{
    public class RegisterViewModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string Patronymic { get; set; }
    }
}