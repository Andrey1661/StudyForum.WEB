using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyForum.Entities
{
    /// <summary>
    /// Модель группы
    /// </summary>
    public class Group
    {
        /// <summary>
        /// Идентификационный номер
        /// </summary>        
        public Guid Id { get; set; }

        /// <summary>
        /// Название группы
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Коллекция ссылок на пользователей, взодящих в группу
        /// </summary>
        public virtual ICollection<User> Users { get; set; }

        /// <summary>
        /// Коллекция ссылок на семестры
        /// </summary>
        public virtual ICollection<GroupSemester> Semesters { get; set; }
    }
}
