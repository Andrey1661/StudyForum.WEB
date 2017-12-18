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
            CreateMap<ThemeModel, ThemeViewModel>();
            CreateMap<SubjectModel, SubjectViewModel>();
            CreateMap<UserModel, ProfileViewModel>()
                .ForMember(dest => dest.FullName,
                    opt => opt.MapFrom(src => $"{src.SecondName} {src.FirstName} {src.Patronymic}".Trim()));
            CreateMap<ThemeModel, ThemeViewModel>();
            CreateMap<MessageModel, MessageViewModel>()
                .ForMember(dest => dest.AuthorName, opt => opt.MapFrom(src => src.Author.FullName));
            CreateMap<AuthorModel, AuthorViewModel>()
                .ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.Role.Name))
                .ForMember(dest => dest.AvatarFilePath, opt => opt.MapFrom(src => src.AvatarFile.PhysicalPath));
            CreateMap<CreateMessageViewModel, MessageModel>()
                .ForMember(dest => dest.AttachedFiles, opt => opt.Ignore());
        }
    }
}