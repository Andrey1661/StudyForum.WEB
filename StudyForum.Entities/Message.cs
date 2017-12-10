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
    /// Сообщение (комментарий) в теме
    /// </summary>
    public class Message
    {
        /// <summary>
        /// Идентификационный номер
        /// </summary>        
        public Guid Id { get; set; }

        /// <summary>
        /// Текстовое содержание
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// Дата создания
        /// </summary>
        public DateTime CreationDate { get; set; }

        /// <summary>
        /// Внешний ключ на автора
        /// </summary>
        public Guid? AuthorId { get; set; }

        /// <summary>
        /// Ссылка на автора
        /// </summary>
        public virtual User Author { get; set; }

        /// <summary>
        /// Внешний ключ на родительское сообщение
        /// </summary>      
        public Guid? ParentMessageId { get; set; }

        /// <summary>
        /// Ссылка на родительское сообщение
        /// </summary>
        public virtual Message ParentMessage { get; set; }

        /// <summary>
        /// Коллекция ссылок на дочерние сообщения
        /// </summary>
        public virtual ICollection<Message> ChildMessages { get; set; }

        public virtual ICollection<MessageFile> Files { get; set; }

        public Guid ThemeId { get; set; }

        public virtual Theme Theme { get; set; }
    }
}
