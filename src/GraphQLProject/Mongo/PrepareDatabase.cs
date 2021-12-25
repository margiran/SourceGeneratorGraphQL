using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace GraphQLProject.Mongo;

public static class PrepareDatabase
{
    public static void PrepareDB(IApplicationBuilder app)
    {
        using (var scopedService = app.ApplicationServices.CreateScope())
        {
            // SeedData(scopedService.ServiceProvider.GetService<ApplicationDbContext>(),isDevelopment );
           scopedService.ServiceProvider.GetService<IDatabaseInitializer>().InitializerAsync();

        }
    }

}