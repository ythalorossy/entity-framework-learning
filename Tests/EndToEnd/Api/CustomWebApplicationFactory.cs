using System.Data.Common;
using System.Linq;
using Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.DependencyInjection;

namespace Tests.EndToEnd.Api;

public class CustomWebApplicationFactory<TProgram>
    : WebApplicationFactory<TProgram> where TProgram : class
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            var dbContextDescriptor = services.SingleOrDefault(d =>
                d.ServiceType == typeof(IDbContextOptionsConfiguration<DataDbContext>));

            services.Remove(dbContextDescriptor);

            var dbConnectionDescriptor = services.SingleOrDefault(d => d.ServiceType == typeof(DbConnection));

            services.Remove(dbConnectionDescriptor);

            services.AddDbContext<DataDbContext>((container, options) =>
            {
                options.UseInMemoryDatabase("ApiTestDatabase");
            });
        });

        builder.UseEnvironment("Development");
    }
}