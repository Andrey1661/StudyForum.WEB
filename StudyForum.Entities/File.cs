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
        /// Физический путь к файлу
        /// </summary>
        public string PhysicalPath { get; set; }

        /// <summary>
        /// Определяет, может ли файл храниться самостоятельно, т.е. без привязки к сообщению или теме
        /// </summary>
        public bool IsIndependent { get; set; }

        /// <summary>
        /// Коллекция ссылок на темы, к которым привязан файл
        /// </summary>
        public virtual ICollection<ThemeFile> Themes { get; set; }

        /// <summary>
        /// Коллекция ссылок на сообщения, к которым привязан файл
        /// </summary>
        public virtual ICollection<MessageFile> Messages { get; set; }
    }
}
