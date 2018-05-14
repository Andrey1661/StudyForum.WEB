using System.Configuration;
using System.Web.Hosting;
using AutoMapper;
using Microsoft.Owin.Security;
using Ninject.Modules;
using Ninject.Web.Common.WebHost;
using StudyForum.DataAccess;
using StudyForum.DataAccess.Core.Abstract.Services;
using StudyForum.DataAccess.Services;

[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(StudyForum.WEB.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethod(typeof(StudyForum.WEB.App_Start.NinjectWebCommon), "Stop")]

namespace StudyForum.WEB.App_Start
{
    using System;
    using System.Web;
    using Microsoft.Web.Infrastructure.DynamicModuleHelper;
    using Ninject;
    using Ninject.Web.Common;
    using DataAccess.Enviroment;
    using System.Web.Http;

    public static class NinjectWebCommon 
    {
        internal static readonly Bootstrapper bootstrapper = new Bootstrapper();

        /// <summary>
        /// Starts the application
        /// </summary>
        public static void Start() 
        {
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
            bootstrapper.Initialize(CreateKernel);
        }
        
        /// <summary>
        /// Stops the application.
        /// </summary>
        public static void Stop()
        {
            bootstrapper.ShutDown();
        }
        
        /// <summary>
        /// Creates the kernel that will manage your application.
        /// </summary>
        /// <returns>The created kernel.</returns>
        private static IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            try
            {
                kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
                kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();

                RegisterServices(kernel);
                return kernel;
            }
            catch
            {
                kernel.Dispose();
                throw;
            }
        }

        /// <summary>
        /// Load your modules or register your services here!
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        private static void RegisterServices(IKernel kernel)
        {
            kernel.Load(new INinjectModule[]
            {
                new NinjectDataAccessModule(ConfigurationManager.ConnectionStrings["local"].ConnectionString), 
            });

            kernel.Bind<IMapper>().ToMethod(ctx =>
            {
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.AddProfile<AutomapperDataAccessModule>();
                    cfg.AddProfile<AutomapperWebModule>();
                });

                return config.CreateMapper();
            }).InSingletonScope();

            kernel.Bind<IUserSevice>().To<UserService>();
            kernel.Bind<IThemeService>().To<ThemeService>();
            kernel.Bind<IMessageService>().To<MessageService>();
            kernel.Bind<ISubjectService>().To<SubjectService>();
            kernel.Bind<ISemesterService>().To<SemesterService>();
            kernel.Bind<IServerFileManager>().To<ServerFileManager>()
                .WithConstructorArgument("serverFileStorage", HostingEnvironment.MapPath("~/App_Data/Files"));
            kernel.Bind<IFileService>().To<FileService>();
            kernel.Bind<IGroupService>().To<GroupService>();
            kernel.Bind<IRepositoryService>().To<RepositoryService>()
                .WithConstructorArgument("repositoryLinkHoursTimeout", 24);

            kernel.Bind<ISignInManager>().To<SignInManager>();
            kernel.Bind<IAuthenticationManager>().ToMethod(ctx =>
            {
                return HttpContext.Current.GetOwinContext().Authentication;
            }).InRequestScope();

            kernel.Bind<GroupDbInitializer>().ToSelf();
        }        
    }
}
