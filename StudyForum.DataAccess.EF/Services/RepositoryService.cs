using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Pagination;
using StudyForum.DataAccess.Core.Abstract.Services;
using StudyForum.DataAccess.Core.Enviroment;
using StudyForum.DataAccess.Core.Enviroment.Filters;
using StudyForum.DataAccess.Core.Models;
using StudyForum.DataAccess.Utils;
using StudyForum.Db.EF;
using StudyForum.Entities;

namespace StudyForum.DataAccess.Services
{
    public class RepositoryService : ServiceBase, IRepositoryService
    {
        private int RepositoryLinkHoursTimeout { get; }
        private IFileService FileService { get; }

        public RepositoryService(ApplicationDbContext context, IMapper mapper, IFileService fileServce, int repositoryLinkHoursTimeout) : base(context, mapper)
        {
            RepositoryLinkHoursTimeout = repositoryLinkHoursTimeout;
            FileService = fileServce;
        }

        public async Task<RepositoryModel> GetRepositoryAsync(Guid repositoryId, Guid userId)
        {
            var rep = await Context.Repositories.Include(r => r.Files.Select(f => f.File))
                .FirstOrDefaultAsync(r => r.Id == repositoryId);

            if (rep == null) return null;

            if (rep.Shared) return Mapper.Map<Repository, RepositoryModel>(rep);

            var user = await Context.Users.Include(u => u.Identity).FirstOrDefaultAsync(u => u.Id == userId);

            if (user != null && 
                (user.Identity.Role.Name == "Admin" || 
                 user.Identity.Role.Name == "Moderator" || 
                 rep.OwnerType == 1 && rep.OwnerId == user.Id))
            {
                return Mapper.Map<Repository, RepositoryModel>(rep);
            }

            return null;

        }

        public async Task<RepositoryModel> GetUserRepositoryAsync(Guid userId)
        {
            var rep = await Context.Repositories
                .Include(r => r.Files.Select(f => f.File.Uploader))
                .FirstOrDefaultAsync(r => r.OwnerType == 1 && r.OwnerId == userId);
            return rep == null ? null : Mapper.Map<RepositoryModel>(rep);
        }

        public async Task<RepositoryModel> GetGroupRepositoryAsync(Guid groupId)
        {
            var rep = await Context.Repositories
                .Include(r => r.Files.Select(f => f.File.Uploader))
                .FirstOrDefaultAsync(r => r.OwnerType == 0 && r.OwnerId == groupId);
            return rep == null ? null : Mapper.Map<RepositoryModel>(rep);
        }

        public async Task<RepositoryModel> GetGroupRepositoryAsync(string groupName)
        {
            var group = await Context.Groups.FirstOrDefaultAsync(g => g.Name == groupName);
            if (group != null)
            {
                var rep = await Context.Repositories
                    .Include(r => r.Files.Select(f => f.File.Uploader))
                    .FirstOrDefaultAsync(r => r.OwnerType == 0 && r.OwnerId == group.Id);
                return rep == null ? null : Mapper.Map<Repository, RepositoryModel>(rep);
            }

            return null;
        }

        public async Task<RepositoryModel> GetRepositoryByLink(Guid linkId)
        {
            var rep = await Context.Repositories
                .Include(r => r.Files.Select(f => f.File.Uploader))
                .FirstOrDefaultAsync(r => r.LinkId == linkId);

            if (rep == null) return null;

            if (!rep.LinkCreationDate.HasValue || 
                DateTime.Now.Subtract(rep.LinkCreationDate.Value).TotalHours > RepositoryLinkHoursTimeout)
            {
                rep.LinkId = null;
                rep.LinkCreationDate = null;
                Context.Entry(rep).State = EntityState.Modified;
                await Context.SaveChangesAsync();
                return null;
            }

            return Mapper.Map<Repository, RepositoryModel>(rep);
        }

        public Task<PagedList<RepositoryModel>> GetRepositoriesAsync(RepositoryFilter filter, ListOptions listOptions)
        {
            throw new NotImplementedException();
        }

        public async Task<PagedList<FileModel>> GetRepositoryFilesAsync(Guid repositoryId, ListOptions listOptions, RepositoryFileFilter filter = null)
        {
            IQueryable<File> files = Context.RepositoryFiles
                .Where(f => f.RepositoryId == repositoryId)
                .Select(f => f.File);

            Expression<Func<File, bool>> predicate = null;

            if (filter != null)
            {
                if (!string.IsNullOrWhiteSpace(filter.SearchString))
                    predicate = predicate.AndAlso(f => f.FileName.ToLower().Contains(filter.SearchString.ToLower()));
                if (!string.IsNullOrWhiteSpace(filter.Extension))
                    predicate.AndAlso(f => f.FileName.Split('.').Last() == filter.Extension);
                if (filter.UploadDateAfter.HasValue)
                    predicate = predicate.AndAlso(f => f.UploadDate >= filter.UploadDateAfter.Value);
                if (filter.UploadDateBefore.HasValue)
                    predicate = predicate.AndAlso(f => f.UploadDate <= filter.UploadDateBefore);

            }

            int count;

            if (predicate != null)
            {
                files = files.Where(predicate);
                count = await Context.FileModels.CountAsync(predicate);
            }
            else
            {
                count = await Context.FileModels.CountAsync();
            }

            var result = listOptions.CreatePagedList<FileModel>(count);

            if (count == 0)
            {
                return result;
            }

            files = files.OrderBy(f => f.UploadDate);
            files = listOptions.TakeFrom(files);

            var list = await files.ToListAsync();
            result.AddRange(Mapper.Map<IEnumerable<File>, IEnumerable<FileModel>>(list));
            return result;
        }

        public async Task AddFileAsync(Guid repositoryId, Guid fileId)
        {
            var rep = await Context.Repositories.FindAsync(repositoryId);
            var file = await Context.FileModels.FindAsync(fileId);

            if (rep != null && file != null)
            {
                var repFile = new RepositoryFile
                {
                    FileId = fileId,
                    RepositoryId = repositoryId
                };

                Context.RepositoryFiles.Add(repFile);
                await Context.SaveChangesAsync();
            }
        }

        public async Task UploadFileAsync(Guid repositoryId, UploadFileModel file)
        {
            var model = await FileService.SaveFileAsync(file);
            Context.RepositoryFiles.Add(new RepositoryFile
            {
                FileId = model.Id,
                RepositoryId = repositoryId
            });
            await Context.SaveChangesAsync();
        }

        public async Task DeleteFileAsync(Guid repositoryId, Guid fileId)
        {
            var repositoryFile =
                await Context.RepositoryFiles.FirstOrDefaultAsync(f =>
                    f.FileId == fileId && f.RepositoryId == repositoryId);

            if (repositoryFile != null)
            {
                Context.RepositoryFiles.Remove(repositoryFile);
                await Context.SaveChangesAsync();
            }
        }

        public Task<Guid> CreateLinkAsync(Guid repositoryId)
        {
            throw new NotImplementedException();
        }

        public async Task SetRepositorySharedAsync(Guid repositoryId, bool shared)
        {
            var rep = await Context.Repositories.FindAsync(repositoryId);
            if (rep != null) rep.Shared = shared;

            Context.Entry(rep).State = EntityState.Modified;
            await Context.SaveChangesAsync();
        }
    }
}
