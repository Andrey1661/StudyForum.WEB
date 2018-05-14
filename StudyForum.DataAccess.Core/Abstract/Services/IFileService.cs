using System;
using System.Collections.Generic;
using System.IO;
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
    /// Интерфейс сервиса, отвечающего за работу с файлами
    /// </summary>
    public interface IFileService
    {
        IServerFileManager ServerFileManager { get; }

        /// <summary>
        /// Создает информационную модель файла в БД и загружает сам файл на сервер
        /// </summary>
        /// <param name="fileModel">Модель загружаемого файла</param>
        /// <returns></returns>
        Task<FileModel> SaveFileAsync(UploadFileModel fileModel);

        Task<IEnumerable<FileModel>> SaveMessageFilesAsync(Guid messageId, ICollection<UploadFileModel> fileModels);

        /// <summary>
        /// Загружает файл с сервера для скачивания
        /// </summary>
        /// <param name="fileId"></param>
        /// <returns></returns>
        Task<DownloadFileModel> LoadFileAsync(Guid fileId);

        /// <summary>
        /// Получает модель файла по id
        /// </summary>
        /// <param name="fileId">id файла</param>
        /// <returns></returns>
        Task<FileModel> GetFileAsync(Guid fileId);

        /// <summary>
        /// Получает коллекцию файлов
        /// </summary>
        /// <param name="listOptions">Параметры для постраничного вывода</param>
        /// <param name="filter">Фильтр запроса</param>
        /// <returns></returns>
        Task<PagedList<FileModel>> GetFilesAsync(ListOptions listOptions, FileFilter filter = null);

        /// <summary>
        /// Получает файлы, прикрпленные к сообщению
        /// </summary>
        /// <param name="messageId">Id сообщения</param>
        /// <param name="listOptions">Параметры для постраничного вывода</param>
        /// <returns></returns>
        Task<PagedList<FileModel>> GetMessageFilesAsync(Guid messageId, ListOptions listOptions);

        /// <summary>
        /// Получает файлы, прикрпленные к теме
        /// </summary>
        /// <param name="themeId">Id темы</param>
        /// <param name="listOptions">Параметры для постраничного вывода</param>
        /// <returns></returns>
        Task<PagedList<FileModel>> GetThemeFilesAsync(Guid themeId, ListOptions listOptions);

        /// <summary>
        /// Получает файлы репозитория
        /// </summary>
        /// <param name="repositoryId">Id репозитория</param>
        /// <param name="listOptions">Параметры для постраничного вывода</param>
        /// <returns></returns>
        Task<PagedList<FileModel>> GetRepositoryFilesAsync(Guid repositoryId, ListOptions listOptions);

        Task<string> GetFilePathAsync(Guid fileId);
    }
}
