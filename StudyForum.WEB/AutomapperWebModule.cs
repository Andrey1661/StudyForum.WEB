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
                .ForMember(dst => dst.FullName,
                    opt => opt.MapFrom(src => $"{src.SecondName} {src.FirstName} {src.Patronymic}".Trim()));
            CreateMap<ThemeModel, ThemeViewModel>();
            CreateMap<MessageModel, MessageViewModel>()
                .ForMember(dst => dst.AuthorName, opt => opt.MapFrom(src => src.Author.FullName));
            CreateMap<AuthorModel, AuthorViewModel>()
                .ForMember(dst => dst.Role, opt => opt.MapFrom(src => src.Role.Name));
            CreateMap<CreateMessageViewModel, CreateMessageModel>();
            CreateMap<HttpPostedFileBase, UploadFileModel>()
                .ForMember(dst => dst.FileStream, opt => opt.MapFrom(src => src.InputStream))
                .ForMember(dst => dst.FileType, opt => opt.MapFrom(src => src.ContentType))
                .ForMember(dst => dst.ContentLength, opt => opt.MapFrom(src => src.ContentLength));
            CreateMap<RepositoryModel, RepositoryViewModel>();
            CreateMap<FileModel, ExtendedFileViewModel>();
        }
    }
}