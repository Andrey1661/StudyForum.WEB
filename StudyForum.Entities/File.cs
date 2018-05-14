using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyForum.Entities
{
    /// <summary>
    /// Модель файла, хранящегося в файловой системе
    /// </summary>
    public class File
    {
        /// <summary>
        /// Идентификационный номер
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Имя файла
        /// </summary>
        [MaxLength(100)]
        public string FileName { get; set; }

        /// <summary>
        /// Тип файла
        /// </summary>
        [MaxLength(200)]
        public string FileType { get; set; }

        /// <summary>
        /// Размер файла
        /// </summary>
        public int ContentLength { get; set; }

        /// <summary>
        /// Коллекция ссылок на темы, к которым привязан файл
        /// </summary>
        public virtual ICollection<ThemeFile> Themes { get; set; }

        /// <summary>
        /// Коллекция ссылок на сообщения, к которым привязан файл
        /// </summary>
        public virtual ICollection<MessageFile> Messages { get; set; }

        /// <summary>
        /// Коллекция ссылок на репозитории, к которым привязан файл
        /// </summary>
        public virtual ICollection<RepositoryFile> Repositories { get; set; }

        /// <summary>
        /// Дата загрузки
        /// </summary>
        public DateTime UploadDate { get; set; }

        /// <summary>
        /// Id пользователя, загрузившего файл
        /// </summary>
        public Guid UploaderId { get; set; }

        /// <summary>
        /// Ссылка на пользователя, загрузившего файл
        /// </summary>
        public virtual User Uploader { get; set; }
    }
}
