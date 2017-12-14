using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StudyForum.DataAccess.Core.Enviroment;
using StudyForum.DataAccess.Core.Models;

namespace StudyForum.DataAccess.Core.Abstract.Services
{
    public interface ISubjectService
    {
        Task<SubjectModel> GetSubjectAsync(Guid subjectId);
        Task<PagedList<SubjectModel>> GetSubjectsAsync(Guid groupId, Guid semesterId, ListOptions listOptions = null);
    }
}
