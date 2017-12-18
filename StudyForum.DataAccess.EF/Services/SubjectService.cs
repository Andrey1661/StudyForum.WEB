using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Common.CommandTrees;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using StudyForum.DataAccess.Core.Abstract.Services;
using StudyForum.DataAccess.Core.Enviroment;
using StudyForum.DataAccess.Core.Models;
using StudyForum.Db.EF;
using StudyForum.Entities;

namespace StudyForum.DataAccess.Services
{
    public class SubjectService : ServiceBase, ISubjectService
    {
        public SubjectService(ApplicationDbContext context, IMapper mapper) : base(context, mapper)
        {
        }

        public async Task<SubjectModel> GetSubjectAsync(Guid subjectId)
        {
            var subject = await Context.Subjects.FindAsync(subjectId);
            return subject == null ? null : Mapper.Map<Subject, SubjectModel>(subject);
        }

        public async Task<PagedList<SubjectModel>> GetSubjectsAsync(Guid groupId, Guid semesterId, ListOptions listOptions)
        {
            var query = Context.SelectSubjectByGroupAndSemester(groupId, semesterId);

            //var query = from subject in Context.Subjects
            //    join gss in Context.GroupSemesterSubjects on subject.Id equals gss.SubjectId
            //    join gs in Context.GroupSemesters on gss.GroupSemesterId equals gs.Id
            //    where gs.GroupId == groupId && gs.SemesterId == semesterId
            //    orderby subject.Name
            //    select new Subject {Id = gss.Id, Name = subject.Name};

            var result = new PagedList<SubjectModel>();

            //if (listOptions != null)
            //{
            //    query = query.Skip(listOptions.Offset).Take(listOptions.PageSize);
            //    result.Page = listOptions.Page;
            //    result.PageSize = listOptions.PageSize;
            //}

            var subjects = await query.ToListAsync();
            result.AddRange(Mapper.Map<IEnumerable<Subject>, IEnumerable<SubjectModel>>(subjects));
            return result;
        }
    }
}
