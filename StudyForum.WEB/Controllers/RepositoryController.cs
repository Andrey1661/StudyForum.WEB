using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using StudyForum.DataAccess.Core.Abstract.Services;
using StudyForum.DataAccess.Core.Enviroment;
using StudyForum.DataAccess.Core.Enviroment.Filters;
using StudyForum.DataAccess.Core.Models;
using StudyForum.WEB.Utils;
using StudyForum.WEB.ViewModels;

namespace StudyForum.WEB.Controllers
{
    public class RepositoryController : Controller
    {
        private IRepositoryService RepositoryService { get; }
        private IMapper Mapper { get; }

        public RepositoryController(IRepositoryService repositoryService, IMapper mapper)
        {
            RepositoryService = repositoryService;
            Mapper = mapper;
        }

        [Authorize]
        public async Task<ActionResult> UserRepository()
        {
            var repository = await RepositoryService.GetUserRepositoryAsync(User.GetId());
            var model = Mapper.Map<RepositoryModel, RepositoryViewModel>(repository);

            return View("Repository", model);
        }

        [Authorize]
        public async Task<ActionResult> UploadFile(Guid repositoryId, HttpPostedFileBase file)
        {
            var model = Mapper.Map<HttpPostedFileBase, UploadFileModel>(file);
            model.UploaderId = User.GetId();
;           await RepositoryService.UploadFileAsync(repositoryId, model);
            return RedirectToAction("UserRepository");
        }

        [HttpGet]
        [Authorize]
        public async Task<PartialViewResult> RepositorySide()
        {
            var rep = await RepositoryService.GetUserRepositoryAsync(User.GetId());
            return PartialView("_RepositorySide", rep.Id);
        }

        [HttpPost]
        public async Task<JsonResult> GetRepositoryFiles(Guid repositoryId, string searchString)
        {
            var files = await RepositoryService.GetRepositoryFilesAsync(repositoryId, new ListOptions(),
                new RepositoryFileFilter { SearchString = searchString });

            if (files == null)
            {
                return Json(null);
            }

            var model = Mapper.Map<ICollection<FileModel>, ICollection<DownloadFileViewModel>>(files);

            return Json(model);
        }
    }
}