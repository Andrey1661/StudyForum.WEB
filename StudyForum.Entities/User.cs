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
    /// Модель данных пользователя
    /// </summary>
    public class User
    {
        /// <summary>
        /// Идентификационный номер, внешний ключ на системную модель пользователя (<see cref="UserIdentity"/>)
        /// </summary>    
        public Guid Id { get; set; }

        /// <summary>
        /// Ссылка на системную модель пользователя (<see cref="UserIdentity"/>)
        /// </summary>
        public virtual UserIdentity Identity { get; set; }

        /// <summary>
        /// Имя
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Фамилия
        /// </summary>   
        public string SecondName { get; set; }

        /// <summary>
        /// Отчество
        /// </summary>  
        public string Patronymic { get; set; }

        /// <summary>
        /// Внешний ключ на группу (<see cref="Group"/>)
        /// </summary> 
        public Guid? GroupId { get; set; }

        /// <summary>
        /// Ссылка на группу (<see cref="Group"/>)
        /// </summary>
        public virtual Group Group { get; set; }

        /// <summary>
        /// Внешний ключ на аватар
        /// </summary> 
        public Guid? AvatarId { get; set; }

        /// <summary>
        /// Ссылка на на файл аватара (<see cref="File"/>)
        /// </summary>
        public virtual File Avatar { get; set; }

        public Guid? RepositoryId { get; set; }

        public Repository Repository { get; set; }

        public virtual ICollection<Theme> Themes { get; set; }

        public virtual ICollection<Message> Messages { get; set; }

        public virtual ICollection<File> Files { get; set; }
    }
}
