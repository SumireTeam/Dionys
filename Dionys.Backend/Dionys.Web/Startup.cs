using AutoMapper;
using Dionys.Infrastructure.Models;
using Dionys.Infrastructure.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SpaServices.ReactDevelopmentServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace Dionys.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }


        // This method gets called by the runtime. Use this method to add services to the container.
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "ASP0000:Do not call 'IServiceCollection.BuildServiceProvider' in 'ConfigureServices'", Justification = "<Pending>")]
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvcCore().SetCompatibilityVersion(CompatibilityVersion.Version_3_0)
                .AddApiExplorer();

            // In production, the React files will be served from this directory
            services.AddSpaStaticFiles(configuration =>
            {
                // Configuration.GetConnectionString("DefaultConnection")
                configuration.RootPath = "ClientApp/build";
            });


            services.AddDbContext<DionysContext>(options =>
                options.UseNpgsql(
                    Configuration.GetConnectionString("DefaultConnection"),
                    b => b.MigrationsAssembly("Dionys.Infrastructure")
                )
            );

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Dionys API", Version = "indev" });
            });

            services.AddTransient<MappingScenario>();
            services.AddTransient<IDionysContext, DionysContext>();
            services.AddTransient<IConsumedProductService, ConsumedProductService>();
            services.AddTransient<IProductService, ProductService>();

            var sp = services.BuildServiceProvider();

            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(sp.GetService<MappingScenario>());
            });

            var mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public static void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //app.UseAuthentication();
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Dionys API v1");
            });

            app.UseStaticFiles();
            app.UseSpaStaticFiles();

            app.UseRouting();
            app.UseEndpoints(endpoints => endpoints.MapControllerRoute("default", "{controller}/{action=Index}/{id?}"));
        }
    }
}
