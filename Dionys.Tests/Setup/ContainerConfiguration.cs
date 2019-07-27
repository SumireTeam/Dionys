using Dionys.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Dionys.Tests.Setup
{
    internal class ContainerConfiguration
    {
        private readonly IServiceCollection _services;

        internal ContainerConfiguration()
        {
            _services = new ServiceCollection();

            _services.AddEntityFrameworkInMemoryDatabase();

            _services.AddTransient<IDionysContext, InMemoryDionysContext>();
            _services.AddTransient<MappingScenario>();
        }

        internal IServiceCollection GetServiceCollection()
        {
            return _services;
        }

    }
}
