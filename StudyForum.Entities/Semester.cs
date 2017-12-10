using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyForum.Entities
{
    /// <summary>
    /// Модель учебного семестра
    /// </summary>
    public class Semester
    {
        /// <summary>
        /// Идентификационный номер
        /// </summary>      
        public Guid Id { get; set; }

        /// <summary>
        /// Название (формат: "Весна 2017г.")
        /// </summary>      
        public string Name { get; set; }

        /// <summary>
        /// Дата начала
        /// </summary>
        public DateTime DateBegin { get; set; }

        /// <summary>
        /// Дата окончания
        /// </summary>
        public DateTime DateEnd { get; set; }

        /// <summary>
        /// Коллекция ссылок на группы
        /// </summary>
        public virtual ICollection<GroupSemester> GroupSemesters { get; set; }
    }
}
