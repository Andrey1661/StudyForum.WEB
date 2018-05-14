using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using StudyForum.DataAccess.Core.Abstract.Services;
using StudyForum.DataAccess.Core.Models;
using StudyForum.WEB.Utils;

namespace StudyForum.WEB.Controllers
{
    public class FileController : Controller
    {
        public IFileService FileService { get; }

        public FileController(IFileService fileService)
        {
            FileService = fileService;
        }

        [Authorize]
        [HttpPost]
        public async Task UploadFile(HttpPostedFileBase file)
        {
            await FileService.SaveFileAsync(new UploadFileModel
            {
                FileStream = file.InputStream,
                FileName = file.FileName,
                UploaderId = User.GetId(),
                FileType = file.ContentType
            });
        }

        public async Task<ActionResult> Files()
        {
            var files = await FileService.GetFilesAsync(null);
            return View(files);
        }

        public async Task<FileResult> DownloadFile(Guid fileId)
        {
            var file = await FileService.LoadFileAsync(fileId);
            return File(file.FileStream, file.FileType, file.FileName);
        }
    }
}