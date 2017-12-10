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
    /// Связующая таблица файлов и тем
    /// </summary>
    public class ThemeFile
    {
        /// <summary>
        /// Внешний ключ на тему
        /// </summary>    
        public Guid ThemeId { get; set; }

        /// <summary>
        /// Ссылка на тему
        /// </summary>
        public virtual Theme Theme { get; set; }

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
