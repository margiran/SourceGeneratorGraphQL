using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace GraphQLProject.SetupServices
{
    public interface IServiceInstallers
    {
         void InstallService(IConfiguration Configuration,IServiceCollection services,IWebHostEnvironment env);
    }
}