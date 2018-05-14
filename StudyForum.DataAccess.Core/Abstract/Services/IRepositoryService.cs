using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pagination;
using StudyForum.DataAccess.Core.Enviroment;
using StudyForum.DataAccess.Core.Enviroment.Filters;
using StudyForum.DataAccess.Core.Models;

namespace StudyForum.DataAccess.Core.Abstract.Services
{
    public interface IRepositoryService
    {
        Task<RepositoryModel> GetRepositoryAsync(Guid repositoryId, Guid userId);
        Task<RepositoryModel> GetUserRepositoryAsync(Guid userId);
        Task<RepositoryModel> GetGroupRepositoryAsync(Guid groupId);
        Task<RepositoryModel> GetGroupRepositoryAsync(string groupName);

        Task<RepositoryModel> GetRepositoryByLink(Guid linkId);

        Task<PagedList<RepositoryModel>> GetRepositoriesAsync(RepositoryFilter filter, ListOptions listOptions);

        Task<PagedList<FileModel>> GetRepositoryFilesAsync(Guid repositoryId, ListOptions listOptions,
            RepositoryFileFilter filter = null);

        Task AddFileAsync(Guid repositoryId, Guid fileId);
        Task UploadFileAsync(Guid repositoryId, UploadFileModel file);
        Task DeleteFileAsync(Guid repositoryId, Guid fileId);

        Task<Guid> CreateLinkAsync(Guid repositoryId);
            
        Task SetRepositorySharedAsync(Guid repositoryId, bool shared);
    }
}
