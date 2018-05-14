using AutoMapper;
using Microsoft.AspNetCore.Http;
using StudyForum.DataAccess.Core.Models;
using StudyForum.WebCore.ViewModels;
using StudyForum.WebCore.ViewModels.Account;
using StudyForum.WebCore.ViewModels.Themes;

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
            CreateMap<MessageModel, MessageViewModel>()
                .ForMember(dst => dst.AuthorName, opt => opt.MapFrom(src => src.Author.FullName));
            CreateMap<AuthorModel, AuthorViewModel>()
                .ForMember(dst => dst.Role, opt => opt.MapFrom(src => src.Role.Name));
            CreateMap<CreateMessageViewModel, CreateMessageModel>();
            CreateMap<IFormFile, UploadFileModel>()
                //.ForMember(dst => dst.FileStream, opt => opt.ResolveUsing(src => src.OpenReadStream()))
                .ForMember(dst => dst.FileType, opt => opt.MapFrom(src => src.ContentType))
                .ForMember(dst => dst.ContentLength, opt => opt.MapFrom(src => src.Length));
            CreateMap<RepositoryModel, RepositoryViewModel>();
            CreateMap<FileModel, ExtendedFileViewModel>();
            CreateMap<GroupModel, GroupViewModel>();
        }
    }
}