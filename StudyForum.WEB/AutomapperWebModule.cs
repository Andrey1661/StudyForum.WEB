using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using StudyForum.DataAccess.Core.Models;
using StudyForum.WEB.ViewModels;

namespace StudyForum.WEB
{
    public class AutomapperWebModule : Profile
    {
        public AutomapperWebModule()
        {
            CreateMap<RegisterViewModel, CreateUserModel>();
        }
    }
}