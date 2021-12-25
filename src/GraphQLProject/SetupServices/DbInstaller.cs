using System;
using GraphQLProject.Models;
using GraphQLProject.Mongo;
using GraphQLProject.Repositories;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace GraphQLProject.SetupServices;

public class DbInstaller : IServiceInstallers
{
    public void InstallService(IConfiguration Configuration, IServiceCollection services, IWebHostEnvironment env)
    {
        services.AddMongoDB(Configuration);
    
        services.AddScoped<IRepository<Category,Guid>,Repository<Category,Guid>>();


    }

}

