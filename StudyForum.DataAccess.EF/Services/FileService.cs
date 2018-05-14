using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
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
using File = StudyForum.Entities.File;

namespace StudyForum.DataAccess.Services
{
    public class FileService : ServiceBase, IFileService
    {
        public IServerFileManager ServerFileManager { get; set; }

        public FileService(ApplicationDbContext context, IMapper mapper, IServerFileManager serverFileManager) : base(context, mapper)
        {
            ServerFileManager = serverFileManager;
        }

        public async Task<string> GetFilePathAsync(Guid fileId)
        {
            var file = await Context.FileModels.FindAsync(fileId);
            if (file != null)
            {
                return ServerFileManager.GetFullName(file.FileName);
            }

            return string.Empty;
        }

        public async Task<FileModel> SaveFileAsync(UploadFileModel fileModel)
        {
            Guid fileId = Guid.NewGuid();
            string serverFileName = $"{fileId}_{fileModel.FileName}";

            await ServerFileManager.SaveFileAsync(fileModel.FileStream, serverFileName);
            DateTime uploadDate = DateTime.Now;
             
            Context.FileModels.Add(new File
            {
                UploadDate = uploadDate,
                UploaderId = fileModel.UploaderId,
                ContentLength = fileModel.ContentLength,
                Id = fileId,
                FileName = fileModel.FileName,
                FileType = fileModel.FileType
            });
            await Context.SaveChangesAsync();

            return new FileModel
            {
                Id = fileId,
                UploadDate = uploadDate,
                UploaderId = fileModel.UploaderId
            };
        }

        public async Task<IEnumerable<FileModel>> SaveMessageFilesAsync(Guid messageId, ICollection<UploadFileModel> fileModels)
        {
            var dictionary = new Dictionary<string, Stream>();

            DateTime uploadDate = DateTime.Now;
            var files = fileModels.Select(f =>
            {
                Guid fileId = Guid.NewGuid();
                dictionary.Add(fileId + "_" + f.FileName, f.FileStream);
                return new File
                {
                    UploadDate = uploadDate,
                    UploaderId = f.UploaderId,
                    Id = fileId,
                    FileName = f.FileName,
                    FileType = f.FileType,
                    ContentLength = f.ContentLength,
                    Messages = new List<MessageFile>
                    {
                        new MessageFile
                        {
                            MessageId = messageId,
                            FileId = fileId
                        }
                    }
                };
            }).ToList();

            await ServerFileManager.SaveFilesAsync(dictionary);
            Context.FileModels.AddRange(files);
            await Context.SaveChangesAsync();

            return files.Select(f => new FileModel
            {
                Id = f.Id,
                UploaderId = f.UploaderId,
                UploadDate = f.UploadDate,
                Name = f.FileName
            }).ToList();
        }

        public async Task<DownloadFileModel> LoadFileAsync(Guid fileId)
        {
            var fileModel = await Context.FileModels.FindAsync(fileId);
            if (fileModel == null) return null;

            var stream = await ServerFileManager.GetFileAsync($"{fileModel.Id}_{fileModel.FileName}");

            return new DownloadFileModel
            {
                FileStream = stream,
                FileName = fileModel.FileName,
                FileType = fileModel.FileType,
                ContentLength = fileModel.ContentLength
            };
        }

        public async Task<FileModel> GetFileAsync(Guid fileId)
        {
            var fileModel = await Context.FileModels.FindAsync(fileId);
            return fileModel == null ? null : Mapper.Map<File, FileModel>(fileModel);
        }

        public async Task<PagedList<FileModel>> GetFilesAsync(ListOptions listOptions, FileFilter filter = null)
        {
            IQueryable<File> query = Context.FileModels;
            Expression<Func<File, bool>> predicate = null;

            if (filter != null)
            {
                if (!string.IsNullOrWhiteSpace(filter.SearchString))
                    predicate = predicate.AndAlso(f => f.FileName.ToLower().Contains(filter.SearchString.ToLower()));
                if (!string.IsNullOrWhiteSpace(filter.Extension))
                    predicate.AndAlso(f => f.FileName.Split('.').Last() == filter.Extension);
                if (filter.MessageId != null)
                {
                    predicate = predicate.AndAlso(f =>
                        f.Messages.Select(m => m.MessageId).Contains(filter.MessageId.Value));
                }
                else if (filter.ThemeId != null)
                {
                    predicate = predicate.AndAlso(f =>
                        f.Messages.Select(m => m.Message.ThemeId).Contains(filter.ThemeId.Value));
                }
                    
            }

            int count;

            if (predicate != null)
            {
                query = query.Where(predicate);
                count = await Context.FileModels.CountAsync(predicate);
            }
            else
            {
                count = await Context.FileModels.CountAsync();
            }

            var list = listOptions.CreatePagedList<FileModel>(count);
            query = listOptions.TakeFrom(query);

            var files = await query.ToListAsync();
            list.AddRange(Mapper.Map<IEnumerable<File>, IEnumerable<FileModel>>(files));
            return list;
        }

        public async Task<PagedList<FileModel>> GetMessageFilesAsync(Guid messageId, ListOptions listOptions)
        {
            int count = await Context.MessageFiles.Distinct().CountAsync(m => m.MessageId == messageId);
            var result = listOptions.CreatePagedList<FileModel>(count);

            if (count == 0)
            {
                return result;
            }

            IQueryable<File> files = Context.MessageFiles.Where(m => m.MessageId == messageId)
                .Distinct()
                .Select(mf => mf.File)
                .OrderBy(f => f.UploadDate);

            files = listOptions.TakeFrom(files);

            var list = await files.ToListAsync();
            result.AddRange(Mapper.Map<IEnumerable<File>, IEnumerable<FileModel>>(list));
            return result;
        }

        public async Task<PagedList<FileModel>> GetThemeFilesAsync(Guid themeId, ListOptions listOptions)
        {
            int count = await Context.MessageFiles.Distinct().CountAsync(m => m.Message.ThemeId == themeId);
            var result = listOptions.CreatePagedList<FileModel>(count);

            if (count == 0)
            {
                return result;
            }

            IQueryable<File> files = Context.MessageFiles.Where(m => m.Message.ThemeId == themeId)
                .Distinct()
                .Select(mf => mf.File)
                .OrderBy(f => f.UploadDate);

            files = listOptions.TakeFrom(files);

            var list = await files.ToListAsync();
            result.AddRange(Mapper.Map<IEnumerable<File>, IEnumerable<FileModel>>(list));
            return result;
        }

        public async Task<PagedList<FileModel>> GetRepositoryFilesAsync(Guid repositoryId, ListOptions listOptions)
        {
            int count = await Context.RepositoryFiles.Distinct().CountAsync(m => m.RepositoryId == repositoryId);

            var result = listOptions.CreatePagedList<FileModel>(count);

            if (count == 0)
            {
                return result;
            }

            IQueryable<File> files = Context.RepositoryFiles
                .Where(f => f.RepositoryId == repositoryId).Distinct()
                .Select(f => f.File)
                .OrderBy(f => f.UploadDate);

            files = listOptions.TakeFrom(files);

            var list = await files.ToListAsync();
            result.AddRange(Mapper.Map<IEnumerable<File>, IEnumerable<FileModel>>(list));
            return result;
        }
    }
}
