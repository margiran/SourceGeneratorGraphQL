using System;
using GraphQLProject.Mongo;
using GraphQLProject.Repositories;
using GraphQLProject.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace GraphQLProject.SetupServices;

public class DbInstaller : IServiceInstallers
{
    public void InstallService(IConfiguration Configuration, IServiceCollection services, IWebHostEnvironment env)
    {
        services.AddMongoDB(Configuration);
        services.AddScoped<ICategoryRepository,CategoryRepository>();
        services.AddScoped<ICategoryService,CategoryService>();

    }

}

