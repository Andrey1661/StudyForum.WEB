using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
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

            Database.SetInitializer<ApplicationDbContext>(null);
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Group> Groups { get; set; }
        public DbSet<GroupSemester> GroupSemesters { get; set; }
        public DbSet<GroupSemesterSubject> GroupSemesterSubjects { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<MessageFile> MessageFiles { get; set; }
        public DbSet<ThemeFile> ThemeFiles { get; set; }
        public DbSet<File> FileModels { get; set; }
        public DbSet<Theme> Themes { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<Semester> Semesters { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserIdentity> Identities { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Repository> Repositories { get; set; }
        public DbSet<RepositoryFile> RepositoryFiles { get; set; }

        public DbSqlQuery<Subject> SelectSubjectByGroupAndSemester(Guid groupId, Guid semesterId)
        {
            return Subjects.SqlQuery("SelectSubjectsBySemesterAndGroup @groupId, @semesterId", 
                new SqlParameter("groupId", groupId),
                new SqlParameter("semesterId", semesterId));
        }
    }
}
