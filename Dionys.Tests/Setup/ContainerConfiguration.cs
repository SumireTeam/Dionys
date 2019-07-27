using System;
using AutoMapper;
using Dionys.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Dionys.Tests.Setup
{
    internal class ContainerConfiguration
    {
        private readonly IServiceProvider _serviceProvider;

        internal ContainerConfiguration()
        {
            IServiceCollection services = new ServiceCollection();

            services.AddEntityFrameworkInMemoryDatabase();

            services.AddDbContext<InMemoryDionysContext>(opt => opt.UseInMemoryDatabase(databaseName: "dionys"));

            services.AddTransient<IDionysContext, InMemoryDionysContext>();
            services.AddTransient<MappingScenario>();

            _serviceProvider = services.BuildServiceProvider();
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(_serviceProvider.GetService<MappingScenario>());
            });

            IMapper mapper = mappingConfig.CreateMapper();

            var mappingConfigNested = new MapperConfiguration(mc =>
            {
                mc.AddProfile(_serviceProvider.GetService<MappingScenario>());
                mc.AddProfile(new NestedMappingScenario(_serviceProvider.GetService<IDionysContext>(), mapper));
            });

            IMapper nestedMapper = mappingConfigNested.CreateMapper();
            services.AddSingleton(nestedMapper);
        }

        internal IServiceProvider GetServiceProvider()
        {
            return _serviceProvider;
        }

    }
}
