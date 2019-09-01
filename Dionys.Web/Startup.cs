using AutoMapper;
using Dionys.Infrastructure.Models;
using Dionys.Infrastructure.Services;
using Dionys.Web.App.Jwt;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
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

            const string signingSecurityKey = "0d5b3235a8b403c3dab9c3f4f65c07fcalskd234n1k41230";
            var signingKey = new SigningSymmetricKey(signingSecurityKey);
            services.AddSingleton<IJwtSigningEncodingKey>(signingKey);
            const string jwtSchemeName = "JwtBearer";
            var signingDecodingKey = (IJwtSigningDecodingKey)signingKey;

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = jwtSchemeName;
                options.DefaultChallengeScheme = jwtSchemeName;
            });

            services.AddDefaultIdentity<User>()
                .AddDefaultUI(UIFramework.Bootstrap4);

            //services.AddIdentityServer().AddCookieAuthentication();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "Dionys API", Version = "indev" });
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
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseAuthentication();
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
