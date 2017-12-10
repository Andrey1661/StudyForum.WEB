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
    /// Роль пользователя (определяет права доступа)
    /// </summary>
    public class Role
    {
        /// <summary>
        /// Идентификационный номер
        /// </summary>       
        public Guid Id { get; set; }

        /// <summary>
        /// Название
        /// </summary>    
        public string Name { get; set; }
    }
}
