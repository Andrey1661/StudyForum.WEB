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
    /// Модель учебного предмета
    /// </summary>
    public class Subject
    {
        /// <summary>
        /// Идентификационный номер
        /// </summary> 
        public Guid Id { get; set; }

        /// <summary>
        /// Название
        /// </summary> 
        public string Name { get; set; }

        /// <summary>
        /// Ссылка на связку группа-семестр
        /// </summary>
        public virtual ICollection<GroupSemesterSubject> GroupSemesters { get; set; }
    }
}
