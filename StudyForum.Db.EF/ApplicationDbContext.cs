using System.Data.Entity;
using System.Reflection;
using StudyForum.Entities;

namespace StudyForum.Db.EF
{
    /// <summary>
    /// Контекст для доступа к базе данных
    /// </summary>
    /// <seealso cref="System.Data.Entity.DbContext" />
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext()
            : base("Data Source=(localdb)\\v11.0;Database=StudyForum.Db;Integrated Security=True;") { }

        public ApplicationDbContext(string nameOrConnectionString) : base(nameOrConnectionString)
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.AddFromAssembly(typeof(ApplicationDbContext).Assembly);

            base.OnModelCreating(modelBuilder);
        }

        public IDbSet<Group> Groups { get; set; }
        public IDbSet<GroupSemester> GroupSemesters { get; set; }
        public IDbSet<GroupSemesterSubject> GroupSemesterSubjects { get; set; }
        public IDbSet<Message> Messages { get; set; }
        public IDbSet<MessageFile> MessageFiles { get; set; }
        public IDbSet<ThemeFile> ThemeFiles { get; set; }
        public IDbSet<File> FileModels { get; set; }
        public IDbSet<Theme> Themes { get; set; }
        public IDbSet<Subject> Subject { get; set; }
        public IDbSet<Semester> Semesters { get; set; }
        public IDbSet<User> Users { get; set; }
        public IDbSet<UserIdentity> Identities { get; set; }
        public IDbSet<Role> Roles { get; set; }
    }
}
