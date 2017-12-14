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
                .ForMember(t => t.Email, conf => conf.MapFrom(t => t.Identity.Email))
                .ForMember(t => t.Role, conf => conf.MapFrom(t => t.Identity.Role))
                .ForMember(t => t.RegistrationDate, conf => conf.MapFrom(t => t.Identity.RegistrationDate));

            CreateMap<CreateUserModel, User>()
                .ForMember(t => t.Identity,
                    conf => conf.MapFrom(t => new UserIdentity {Email = t.Email, PasswordHash = t.Password}));

            CreateMap<Theme, ThemeModel>();

            CreateMap<MessageModel, Message>();
            CreateMap<Message, MessageModel>()
                .ForMember(t => t.AttachedFiles, conf => conf.MapFrom(t => t.Files.Select(f => f.File)));

            CreateMap<File, FileModel>();
            CreateMap<FileModel, File>();

            CreateMap<Subject, SubjectModel>();
        }
    }
}
