using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Results;
using AutoMapper;
using StudyForum.DataAccess.Core.Abstract.Services;
using StudyForum.DataAccess.Core.Enviroment;
using StudyForum.DataAccess.Core.Enviroment.Filters;
using StudyForum.DataAccess.Core.Models;
using StudyForum.WEB.ViewModels;
using StudyForum.WEB.ViewModels.Filters;

namespace StudyForum.WEB.Controllers.Api
{
    public class RepositoryApiController : ApiController
    {
        private IRepositoryService RepositoryService { get; }
        private IMapper Mapper { get; }

        public RepositoryApiController(IRepositoryService repositoryService, IMapper mapper)
        {
            RepositoryService = repositoryService;
            Mapper = mapper;
        }
    }
}