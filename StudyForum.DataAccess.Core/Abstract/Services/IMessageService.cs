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
    /// <summary>
    /// Интерфейс сервиса, отвечающего за работу с сообщениями
    /// </summary>
    public interface IMessageService
    {
        IFileService FileService { get; }

        /// <summary>
        /// Создает новое сообщение
        /// </summary>
        /// <param name="model">Модельсообщения</param>
        /// <returns></returns>
        Task<Guid> CreateMessageAsync(CreateMessageModel model);

        /// <summary>
        /// Получает сообщение по Id
        /// </summary>
        /// <param name="messageId">Id сообщения</param>
        /// <returns></returns>
        Task<MessageModel> GetMessageAsync(Guid messageId);

        /// <summary>
        /// Получает родительское сообщение
        /// </summary>
        /// <param name="childMessageId">Id дочернего сообщения</param>
        /// <returns></returns>
        Task<MessageModel> GetParentMessageAsync(Guid childMessageId);

        /// <summary>
        /// Получает дочерние сообщения
        /// </summary>
        /// <param name="parentMessageId">Id родительского сообщения</param>
        /// <param name="listOptions">Параметры для постраничного вывода</param>
        /// <returns></returns>
        Task<PagedList<MessageModel>> GetChildMessagesAsync(Guid parentMessageId, ListOptions listOptions = null);

        /// <summary>
        /// Получает коллекцию сообщений
        /// </summary>
        /// <param name="filter">Фильтр запроса</param>
        /// <param name="listOptions">Параметры для постраничного вывода</param>
        /// <returns></returns>
        Task<PagedList<MessageModel>> GetMessagesAsync(MessageFilter filter = null, ListOptions listOptions = null);

        /// <summary>
        /// Получает коллекцию сообщений
        /// </summary>
        /// <param name="listOptions">Параметры для постраничного вывода</param>
        /// <returns></returns>
        Task<PagedList<MessageModel>> GetMessagesAsync(ListOptions listOptions);

        /// <summary>
        /// Получает сообщения, принадлежащией заданной теме
        /// </summary>
        /// <param name="themeId">Id темы</param>
        /// <param name="listOptions">Параметры для постраничного вывода</param>
        /// <returns></returns>
        Task<PagedList<MessageModel>> GetThemeMessagesAsync(Guid themeId, ListOptions listOptions = null);
    }
}
