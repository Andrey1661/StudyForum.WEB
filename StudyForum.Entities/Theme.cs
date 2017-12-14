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
    /// Модель темы обсуждения
    /// </summary>
    public class Theme
    {
        /// <summary>
        /// Идентификационный номер
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Название
        /// </summary>  
        public string Title { get; set; }

        public string Description { get; set; }

        /// <summary>
        /// Дата создания
        /// </summary>
        public DateTime CreationDate { get; set; }

        public Guid SubjectId { get; set; }

        public virtual GroupSemesterSubject Subject { get; set; }

        /// <summary>
        /// Внешний ключ на автора
        /// </summary>  
        public Guid? AuthorId { get; set; }

        /// <summary>
        /// Ссылка на автора
        /// </summary>
        public virtual User Author { get; set; }

        /// <summary>
        /// Коллекция ссылок на сообщения
        /// </summary>
        public virtual ICollection<Message> Messages { get; set; }

        public virtual ICollection<ThemeFile> Files { get; set; }
    }
}
