using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyForum.Entities
{
    /// <summary>
    /// Системная модель пользователя, служащая для идентификации в системе
    /// </summary>
    public class UserIdentity
    {
        /// <summary>
        /// Идентификационный номер, внешний ключ на модель данных пользователя (<see cref="User"/>)
        /// </summary> 
        public Guid Id { get; set; }

        /// <summary>
        /// Ссылка на модель данных пользователя (<see cref="User"/>)
        /// </summary>
        public virtual User User { get; set; }

        /// <summary>
        /// Электронный адрес пользователя
        /// </summary> 
        public string Email { get; set; }

        /// <summary>
        /// Определяет, подтвержден ли email пользователя
        /// </summary>
        public bool EmailConfirmed { get; set; }

        /// <summary>
        /// Хешированный пароль
        /// </summary>
        public string PasswordHash { get; set; }

        /// <summary>
        /// Дата регистрации
        /// </summary>
        public DateTime RegistrationDate { get; set; }

        /// <summary>
        /// Ключ, используемый при смене пароля
        /// </summary>
        public Guid? PasswordResetVerificationToken { get; set; }

        /// <summary>
        /// Внешний ключ на роль пользователя (<see cref="Role"/>)
        /// </summary>    
        public virtual Guid RoleId { get; set; }

        /// <summary>
        /// Ссылка на роль пользователя (<see cref="Role"/>)
        /// </summary>
        public virtual Role Role { get; set; }
    }
}
