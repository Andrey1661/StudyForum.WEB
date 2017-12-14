using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using StudyForum.Db.EF;

namespace StudyForum.DataAccess.Services
{
    public abstract class ServiceBase
    {
        protected IMapper Mapper { get; }
        protected ApplicationDbContext Context { get; }

        protected ServiceBase(ApplicationDbContext context, IMapper mapper)
        {
            Context = context;
            Mapper = mapper;
        }
    }
}
