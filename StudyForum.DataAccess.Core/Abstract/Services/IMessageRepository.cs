using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StudyForum.DataAccess.Core.Enviroment;
using StudyForum.DataAccess.Core.Models;

namespace StudyForum.DataAccess.Core.Abstract.Services
{
    public interface IMessageRepository
    {
        Task CreateMessageModel();
        Task<MessageModel> GetMessage(Guid messageId);
        Task<MessageModel> GetParentMessage(Guid childMessageId);
        Task<PagedList<MessageModel>> GetChildMessages(Guid parentMessageId, ListOptions listOptions);
        Task<PagedList<MessageModel>> GetMessages(Guid themeId, ListOptions listOptions);
    }
}
