using System;
using System.IO;
using System.Reflection;
using System.Text;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.SpaServices.Webpack;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using StudyForum.DataAccess.Core.Abstract.Services;
using StudyForum.DataAccess.Enviroment;
using StudyForum.DataAccess.Services;
using StudyForum.Db.EF;
using StudyForum.WEB;
using Swashbuckle.AspNetCore.Swagger;

namespace StudyForum_WebCore
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

            services.AddScoped(s =>
            {
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.AddProfile<AutomapperDataAccessModule>();
                    cfg.AddProfile<AutomapperWebModule>();
                });

                return config.CreateMapper();
            });
            services.AddScoped(s => new ApplicationDbContext(Configuration.GetConnectionString("Local")));

            RegisterDAServices(services);

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateActor = true,
                    ValidateAudience = true,
                    ValidateIssuer = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = Configuration["Issuer"],
                    ValidAudience = Configuration["Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["SigninKey"]))
                };
            });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info {Title = "StudyForumApi", Version = "v1"});
                string xmlFile = $"{Assembly.GetEntryAssembly().GetName().Name}.xml";
                string xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseWebpackDevMiddleware(new WebpackDevMiddlewareOptions
                {
                    HotModuleReplacement = true
                });
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "StudyForumApi");
            });

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");

                routes.MapSpaFallbackRoute(
                    name: "spa-fallback",
                    defaults: new { controller = "Home", action = "Index" });
            });
        }

        private void RegisterDAServices(IServiceCollection services)
        {
            services.AddScoped<IUserSevice, UserService>();
            services.AddScoped<IThemeService, ThemeService>();
            services.AddScoped<IMessageService, MessageService>();
            services.AddScoped<ISubjectService, SubjectService>();
            services.AddScoped<ISemesterService, SemesterService>();
            services.AddScoped<IServerFileManager, ServerFileManager>(s =>
                new ServerFileManager(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "~/Data/Files")));

            services.AddScoped<IFileService, FileService>();
            services.AddScoped<IGroupService, GroupService>();
            services.AddScoped<IRepositoryService, RepositoryService>(s =>
                new RepositoryService(
                    s.GetService<ApplicationDbContext>(), 
                    s.GetService<IMapper>(),
                    s.GetService<IFileService>(), 24));
        }
    }
}
