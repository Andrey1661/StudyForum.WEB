using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Ninject.Modules;
using StudyForum.DataAccess.Core.Abstract.Services;
using StudyForum.Db.EF;

namespace StudyForum.DataAccess.Enviroment
{
    public class NinjectDataAccessModule : NinjectModule
    {
        private readonly string _connectionString;

        public NinjectDataAccessModule(string connectionString)
        {
            _connectionString = connectionString;
        }

        public override void Load()
        {
            Kernel.Bind<ApplicationDbContext>().ToSelf()
                .WithConstructorArgument("nameOrConnectionString", _connectionString);
        }
    }
}
