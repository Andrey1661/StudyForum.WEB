using System;

namespace StudyForum.Entities
{
    public class RepositoryFile
    {
        public Guid RepositoryId { get; set; }
        public Guid FileId { get; set; }
        public virtual Repository Repository { get; set; }
        public virtual File File { get; set; }
    }
}