using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using StudyForum.DataAccess.Core.Models;
using StudyForum.Entities;

namespace StudyForum.DataAccess.Enviroment
{
    public class AutomapperDataAccessModule : Profile
    {
        public AutomapperDataAccessModule()
        {
            CreateMap<User, UserModel>()
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Identity.Email))
                .ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.Identity.Role))
                .ForMember(dest => dest.RegistrationDate, opt => opt.MapFrom(src => src.Identity.RegistrationDate));

            CreateMap<CreateUserModel, User>()
                .ForMember(dest => dest.Identity,
                    opt => opt.MapFrom(t => new UserIdentity {Email = t.Email}));

            CreateMap<Theme, ThemeModel>();

            CreateMap<CreateMessageModel, Message>();
            CreateMap<Message, MessageModel>()
                .ForMember(dest => dest.AttachedFiles, opt => opt.MapFrom(src => src.Files.Select(f => f.File)));

            CreateMap<File, FileModel>();
            CreateMap<FileModel, File>();

            CreateMap<Subject, SubjectModel>();
            CreateMap<Semester, SemesterModel>();

            CreateMap<Theme, ThemeModel>();
            CreateMap<User, AuthorModel>()
                .ForMember(dest => dest.FullName,
                    opt => opt.MapFrom(src => $"{src.SecondName} {src.FirstName} {src.Patronymic}".Trim()))
                .ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.Identity.Role));

            CreateMap<File, FileModel>().ForMember(dst => dst.Name, opt => opt.MapFrom(src => src.FileName));
            CreateMap<Repository, RepositoryModel>()
                .ForMember(dest => dest.Files, opt => opt.MapFrom(src => src.Files.Select(f => f.File)));
        }
    }
}
