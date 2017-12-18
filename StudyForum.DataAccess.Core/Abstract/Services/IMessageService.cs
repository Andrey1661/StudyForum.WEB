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
    public interface IMessageService
    {
        Task<Guid> CreateMessageAsync(MessageModel model);
        Task<MessageModel> GetMessageAsync(Guid messageId);
        Task<MessageModel> GetParentMessageAsync(Guid childMessageId);
        Task<PagedList<MessageModel>> GetChildMessagesAsync(Guid parentMessageId, ListOptions listOptions = null);
        Task<PagedList<MessageModel>> GetMessagesAsync(MessageFilter filter = null, ListOptions listOptions = null);
        Task<PagedList<MessageModel>> GetMessagesAsync(ListOptions listOptions);
        Task<PagedList<MessageModel>> GetThemeMessagesAsync(Guid themeId, ListOptions listOptions = null);
    }
}
