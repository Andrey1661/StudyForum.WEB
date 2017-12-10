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
    /// Связующая таблица файлов и сообщений
    /// </summary>
    public class MessageFile
    {
        /// <summary>
        /// Внешний ключ на сообщение
        /// </summary>      
        public Guid MessageId { get; set; }

        /// <summary>
        /// Ссылка на сообщение
        /// </summary>
        public virtual Message Message { get; set; }
        /// <summary>
        /// Внешний ключ на файл
        /// </summary>
        
        public Guid FileId { get; set; }
        /// <summary>
        /// Ссылка на файл
        /// </summary>
        public virtual File File { get; set; }
    }
}
