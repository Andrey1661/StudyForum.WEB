using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StudyForum.DataAccess.Core.Models;

namespace StudyForum.DataAccess.Core.Abstract.Services
{
    public interface IServerFileManager
    {
        Task SaveFileAsync(Stream fileStream, string fileName);

        Task OverrideFileAsync(Stream fileStream, string fileName);

        Task DeleteFileAsync(string fileName);

        Task SaveFilesAsync(IDictionary<string, Stream> files);

        Task<Stream> GetFileAsync(string fileName);

        Task<Stream> GetFilesArchiveAsync(string archiveName, params string[] fileNames);

        string GetFullName(string name);
    }
}
