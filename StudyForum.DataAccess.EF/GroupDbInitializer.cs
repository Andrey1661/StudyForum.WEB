using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFUTimetableParser;
using StudyForum.Db.EF;
using StudyForum.Entities;

namespace StudyForum.DataAccess
{
    public class GroupDbInitializer
    {
        private ApplicationDbContext Context { get; }
        private const string XmlFilesLocation = @"C:\Timetable\xml";

        public GroupDbInitializer(ApplicationDbContext context)
        {
            Context = context;
        }

        public async Task CreateSemesterData()
        {
            if (await Context.Groups.AnyAsync())
                return;

            var dataManager = new DataManager();
            var files = Directory.GetFiles(XmlFilesLocation, "*.xml");
            var data = (await dataManager.LoadFromXmlAsync(files)).ToList();

            var semesterId = Guid.NewGuid();
            var semester = new Semester
            {
                Name = "Весна 2017г.",
                DateBegin = DateTime.Parse("01.02.2018"),
                DateEnd = DateTime.Parse("30.06.2018"),
                Id = semesterId
            };
            Context.Semesters.Add(semester);

            var subjects = data.SelectMany(t => t.Subjects).Distinct().Select(s => new Subject
            {
                Id = Guid.NewGuid(),
                Name = s
            }).Where(s => s.Name != null).ToList();

            Context.Subjects.AddRange(subjects);

            var groups = new List<Group>();
            var groupSemesters = new List<GroupSemester>();
            var groupSemesterSubjects = new List<GroupSemesterSubject>();

            foreach (var gr in data)
            {
                var group = new Group
                {
                    Id = Guid.NewGuid(),
                    Name = gr.GroupName
                };
                groups.Add(group);

                var groupSemester = new GroupSemester
                {
                    Id = Guid.NewGuid(),
                    GroupId = group.Id,
                    SemesterId = semesterId
                };
                groupSemesters.Add(groupSemester);

                //var repository = new Repository
                //{
                //    Id = Guid.NewGuid(),
                //    OwnerId = groupSemester.Id,
                //    OwnerType = 0
                //};

                var subj = subjects.Join(gr.Subjects, s => s.Name, name => name, (s, sn) => s).ToList();
                var gss = subj.Select(s => new GroupSemesterSubject
                {
                    Id = Guid.NewGuid(),
                    SubjectId = s.Id,
                    GroupSemesterId = groupSemester.Id
                });
                groupSemesterSubjects.AddRange(gss);
            }

            Context.Groups.AddRange(groups);
            Context.GroupSemesters.AddRange(groupSemesters);
            Context.GroupSemesterSubjects.AddRange(groupSemesterSubjects);

            try
            {
                await Context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            
        }
    }
}
