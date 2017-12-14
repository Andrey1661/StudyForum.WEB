using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StudyForum.DataAccess.Core.Enviroment;
using StudyForum.DataAccess.Core.Enviroment.Filters;
using StudyForum.DataAccess.Core.Models;

namespace StudyForum.DataAccess.Core.Abstract.Services
{
    public interface IFileService
    {
        Task<FileModel> GetFileAsync(Guid fileId);
        Task<PagedList<FileModel>> GetFilesAsync(ListOptions listOptions, FileFilter filter);
        Task<PagedList<FileModel>> GetMessageFilesAsync(Guid messageId, ListOptions listOptions);
        Task<PagedList<FileModel>> GetThemeFilesAsync(Guid themeId, ListOptions listOptions);
    }
}
