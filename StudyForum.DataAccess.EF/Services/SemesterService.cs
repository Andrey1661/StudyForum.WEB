using System;
using System.Data.Entity;
using System.Threading.Tasks;
using AutoMapper;
using StudyForum.DataAccess.Core.Abstract.Services;
using StudyForum.DataAccess.Core.Enviroment;
using StudyForum.DataAccess.Core.Enviroment.Filters;
using StudyForum.DataAccess.Core.Models;
using StudyForum.Db.EF;
using StudyForum.Entities;

namespace StudyForum.DataAccess.Services
{
    public class SemesterService : ServiceBase, ISemesterService
    {
        public SemesterService(ApplicationDbContext context, IMapper mapper) : base(context, mapper)
        {
        }

        public async Task<SemesterModel> GetSemesterAsync(Guid semesterId)
        {
            var semester = await Context.Semesters.FindAsync(semesterId);
            return semester == null ? null : Mapper.Map<Semester, SemesterModel>(semester);
        }

        public async Task<SemesterModel> GetSemesterByNameAsync(string name)
        {
            var semester = await Context.Semesters.FirstOrDefaultAsync(s => s.Name == name);
            return semester == null ? null : Mapper.Map<Semester, SemesterModel>(semester);
        }

        public async Task<SemesterModel> GetCurrentSemesterAsync()
        {
            var now = DateTime.Today;
            var semester = await Context.Semesters.FirstOrDefaultAsync(s => s.DateBegin <= now && s.DateEnd >= now);
            return semester == null ? null : Mapper.Map<Semester, SemesterModel>(semester);
        }

        public async Task<PagedList<SemesterModel>> GetSemestersAsync(ListOptions listOptions, SemesterFilter filter)
        {
            throw new NotImplementedException();
        }
    }
}
