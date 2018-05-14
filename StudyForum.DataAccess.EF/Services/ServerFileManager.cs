using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using StudyForum.DataAccess.Core.Abstract.Services;

namespace StudyForum.DataAccess.Services
{
    public class ServerFileManager : IServerFileManager
    {
        private string ServerFileStorage { get; }

        public ServerFileManager(string serverFileStorage)
        {
            ServerFileStorage = serverFileStorage;
        }

        public async Task SaveFileAsync(Stream fileStream, string fileName)
        {
            await WriteToFileStream(fileStream, fileName, FileMode.CreateNew);
        }

        public async Task OverrideFileAsync(Stream fileStream, string fileName)
        {
            await WriteToFileStream(fileStream, fileName, FileMode.Open);
        }

        public async Task DeleteFileAsync(string fileName)
        {
            await Task.Run(() =>
            {
                if (!File.Exists(fileName))
                {
                    throw new FileNotFoundException();
                }

                File.Delete(fileName);
            });
        }

        public async Task SaveFilesAsync(IDictionary<string, Stream> files)
        {
            var tasks = files.Select(file =>
            {
                return Task.Run(() =>
                {
                    string fileName = file.Key;
                    Stream fileStream = file.Value;

                    using (var stream = new FileStream(GetFullFileName(fileName), FileMode.CreateNew, FileAccess.Write))
                    {
                        fileStream.CopyTo(stream);
                    }

                    fileStream.Dispose();
                });
            });

            await Task.WhenAll(tasks);
        }

        public async Task<Stream> GetFileAsync(string fileName)
        {
            string fullName = Path.Combine(ServerFileStorage, fileName);
            using (Stream stream = new FileStream(fullName, FileMode.Open, FileAccess.Read))
            {
                var memStream = new MemoryStream();
                await stream.CopyToAsync(memStream);
                memStream.Seek(0, SeekOrigin.Begin);
                return memStream;
            }
        }

        public Task<Stream> GetFilesArchiveAsync(string archiveName, params string[] fileNames)
        {
            throw new NotImplementedException();
        }

        public string GetFullName(string name)
        {
            string fullName = Path.Combine(ServerFileStorage, name);
            if (File.Exists(fullName)) return fullName;

            return string.Empty;
        }

        private async Task WriteToFileStream(Stream fileStream, string fileName, FileMode fileMode)
        {
            using (var stream = new FileStream(GetFullFileName(fileName), fileMode, FileAccess.Write))
            {
                await fileStream.CopyToAsync(stream);
            }

            fileStream.Dispose();
        }

        private string GetFullFileName(string name)
        {
            return Path.Combine(ServerFileStorage, name);
        }
    }
}
