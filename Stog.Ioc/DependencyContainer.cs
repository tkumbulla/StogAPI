using Microsoft.Extensions.DependencyInjection;
using Stog.Application.Interfaces.Authentication;
using Stog.Application.Services.Authentication;
using Stog.Data.Context;
using Stog.Data.Repositories;
using Stog.Domain.Interfaces;
using DbContext = Stog.Data.Context.DBContext;
namespace Stog.Ioc
{
    /// <summary>
    /// Class responsible for configuring dependencies.
    /// </summary>
    public static class DependencyContainer
    {
        /// <summary>
        /// All the services will be registered here.
        /// </summary>
        /// <param name="services">The service collection</param>
        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddScoped<IAuthenticationService, AuthenticationService>();
            services.AddScoped<IDbContext, DbContext>();
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        }
    }
}
