using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace StudyForum.WEB.ViewModels
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Укажите адрес электронной почты")]
        [MaxLength(200, ErrorMessage = "Максимальная длина - 200 символов")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Укажите пароль")]
        public string Password { get; set; }

        [Compare("Password", ErrorMessage = "Пароли не совпадают")]
        public string RepeatPassword { get; set; }

        [Required(ErrorMessage = "Укажите имя")]
        [MaxLength(200, ErrorMessage = "Максимальная длина - 200 символов")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Укажите фамилию")]
        [MaxLength(200, ErrorMessage = "Максимальная длина - 200 символов")]
        public string SecondName { get; set; }

        [MaxLength(200, ErrorMessage = "Максимальная длина - 200 символов")]
        public string Patronymic { get; set; }

        public Guid? GroupId { get; set; }
    }
}