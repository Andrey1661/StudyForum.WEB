﻿using System;
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
    public class MessageService : ServiceBase, IMessageService
    {
        public IFileService FileService { get; }

        public MessageService(ApplicationDbContext context, IMapper mapper, IFileService fileService) : base(context, mapper)
        {
            FileService = fileService;
        }

        public async Task<Guid> CreateMessageAsync(CreateMessageModel model)
        {
            var message = Mapper.Map<CreateMessageModel, Message>(model);
            message.Id = Guid.NewGuid();
            message.CreationDate = DateTime.Now;

            foreach (var attachedFile in model.AttachedFiles)
            {
                attachedFile.UploaderId = model.AuthorId;
            }

            Context.Messages.Add(message);
            await Context.SaveChangesAsync();
            await FileService.SaveMessageFilesAsync(message.Id, model.AttachedFiles);

            return message.Id;
        }

        public async Task<MessageModel> GetMessageAsync(Guid messageId)
        {
            var message = await Context.Messages
                .Include(m => m.Author)
                .Include(m => m.Files.Select(f => f.File))
                .FirstOrDefaultAsync(m => m.Id == messageId);
            return message == null ? null : Mapper.Map<Message, MessageModel>(message);
        }

        public async Task<MessageModel> GetParentMessageAsync(Guid childMessageId)
        {
            var message = await Context.Messages.Include(m => m.ParentMessage).FirstOrDefaultAsync();
            return message?.ParentMessage == null ? null : Mapper.Map<Message, MessageModel>(message.ParentMessage);
        }

        public async Task<PagedList<MessageModel>> GetChildMessagesAsync(Guid parentMessageId, ListOptions listOptions)
        {
            var query = Context.Messages.Include(m => m.Author).Include(m => m.Files).AsQueryable();
            var list = new PagedList<MessageModel>();
            list.TotalItemCount = await Context.Messages.CountAsync(m => m.ParentMessageId == parentMessageId);

            if (list.TotalItemCount == 0) return list;

            if (listOptions != null)
            {
                list.Page = listOptions.Page;
                list.PageSize = listOptions.PageSize;
                query = query.OrderByDescending(t => t.CreationDate).Skip(listOptions.Offset).Take(listOptions.PageSize);
            }

            query = query.Where(m => m.ParentMessageId == parentMessageId);

            var messages = await query.ToArrayAsync();
            list.AddRange(Mapper.Map<IEnumerable<Message>, IEnumerable<MessageModel>>(messages));
            return list;
        }

        public Task<PagedList<MessageModel>> GetMessagesAsync(ListOptions listOptions)
        {
            return GetMessagesAsync(null, listOptions);
        }

        public async Task<PagedList<MessageModel>> GetMessagesAsync(MessageFilter filter, ListOptions listOptions)
        {
            var query = Context.Messages.Include(m => m.Author).Include(m => m.Files.Select(f => f.File)).AsQueryable();
            var list = new PagedList<MessageModel>();
            Expression<Func<Message, bool>> predicate = null;

            if (list.TotalItemCount == 0) return list;

            if (listOptions != null)
            {
                list.Page = listOptions.Page;
                list.PageSize = listOptions.PageSize;
                query = query.OrderByDescending(t => t.CreationDate).Skip(listOptions.Offset).Take(listOptions.PageSize);
            }

            if (filter != null)
            {
                if (!string.IsNullOrEmpty(filter.SearchString))
                    predicate = predicate.AndAlso(m => m.Content.Contains(filter.SearchString));
                if (filter.AttachedFilesCount.HasValue) predicate = predicate.AndAlso(m => m.Files.Count == filter.AttachedFilesCount);
                if (filter.AuthorId.HasValue) predicate = predicate.AndAlso(m => m.AuthorId == filter.AuthorId);
                if (filter.ThemeId.HasValue) predicate = predicate.AndAlso(m => m.ThemeId == filter.ThemeId);
                if (filter.CreateDateAfter.HasValue) predicate = predicate.AndAlso(m => m.CreationDate >= filter.CreateDateAfter);
                if (filter.CreateDateBefore.HasValue) predicate = predicate.AndAlso(m => m.CreationDate <= filter.CreateDateBefore);
            }

            if (predicate != null)
            {
                query = query.Where(predicate);
                list.TotalItemCount = await Context.Messages.CountAsync(predicate);
            }
            else
            {
                list.TotalItemCount = await Context.Messages.CountAsync();
            }

            var messages = await query.ToArrayAsync();
            list.AddRange(Mapper.Map<IEnumerable<Message>, IEnumerable<MessageModel>>(messages));
            return list;
        }

        public async Task<PagedList<MessageModel>> GetThemeMessagesAsync(Guid themeId, ListOptions listOptions)
        {
            var query = Context.Messages.Include(m => m.Author).Include(m => m.Files).AsQueryable();
            var list = new PagedList<MessageModel>
            {
                TotalItemCount = await Context.Messages.CountAsync(m => m.ThemeId == themeId)
            };

            if (list.TotalItemCount == 0) return list;

            if (listOptions != null)
            {
                list.Page = listOptions.Page;
                list.PageSize = listOptions.PageSize;
                query = query.OrderByDescending(t => t.CreationDate).Skip(listOptions.Offset).Take(listOptions.PageSize);
            }

            query = query.Where(m => m.ThemeId == themeId);

            var messages = await query.ToArrayAsync();
            list.AddRange(Mapper.Map<IEnumerable<Message>, IEnumerable<MessageModel>>(messages));
            return list;
        }
    }
}
