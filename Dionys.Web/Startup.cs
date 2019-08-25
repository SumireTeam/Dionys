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
using Swashbuckle.AspNetCore.Swagger;

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
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvcCore().SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
                .AddJsonFormatters()
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
                c.SwaggerDoc("v1", new Info { Title = "Dionys API", Version = "indev" });
            });

            services.AddTransient<MappingScenario>();
            services.AddTransient<NestedMappingScenario>();
            services.AddTransient<IDionysContext, DionysContext>();
            services.AddTransient<IConsumedProductService, ConsumedProductService>();
            services.AddTransient<IProductService, ProductService>();


            var sp = services.BuildServiceProvider();

            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(sp.GetService<MappingScenario>());
            });

            IMapper mapper = mappingConfig.CreateMapper();

            var mappingConfigNested = new MapperConfiguration(mc =>
            {
                mc.AddProfile(sp.GetService<MappingScenario>());
                mc.AddProfile(new NestedMappingScenario(sp.GetService<IDionysContext>(), mapper));
            });

            IMapper nestedMapper = mappingConfigNested.CreateMapper();
            services.AddSingleton(nestedMapper);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Dionys API indev");
            });

            app.UseStaticFiles();
            app.UseSpaStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller}/{action=Index}/{id?}");
            });

            app.UseSpa(spa =>
            {
                spa.Options.SourcePath = "ClientApp";

                if (env.IsDevelopment())
                    spa.UseReactDevelopmentServer(npmScript: "start");
            });
        }
    }
}
