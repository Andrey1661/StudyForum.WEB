using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyForum.Db.EF
{
    public class TestEntity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        public virtual TestEntity Parent { get; set; }

        [ForeignKey("Parent")]
        public int? ParentId { get; set; }
    }
}
