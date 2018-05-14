using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyForum.Entities
{
    public class Repository
    {
        public Guid Id { get; set; }

        public Guid OwnerId { get; set; }

        public int OwnerType { get; set; }

        public bool Shared { get; set; }

        public ICollection<RepositoryFile> Files { get; set; }

        public Guid? LinkId { get; set; }
        public DateTime? LinkCreationDate { get; set; }
    }
}
