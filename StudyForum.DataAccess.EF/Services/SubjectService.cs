using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
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
            var query = Context.GroupSemesters.Include(t => t.Subjects.Select(s => s.Subject))
                .Where(t => t.GroupId == groupId && t.SemesterId == semesterId)
                .SelectMany(t => t.Subjects, (gs, gss) => gss.Subject);

            var result = new PagedList<SubjectModel>();

            if (listOptions != null)
            {
                query = query.Skip(listOptions.Offset).Take(listOptions.PageSize);
                result.Page = listOptions.Page;
                result.PageSize = listOptions.PageSize;
            }

            var subjects = await query.ToListAsync();
            result.AddRange(Mapper.Map<IEnumerable<Subject>, IEnumerable<SubjectModel>>(subjects));
            return result;
        }
    }
}
