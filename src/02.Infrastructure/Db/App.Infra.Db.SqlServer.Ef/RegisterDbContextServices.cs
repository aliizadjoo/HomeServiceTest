using App.Domain.Core.Configurations;
using App.Infra.Db.SqlServer.Ef.DbContextAgg;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Identity;

namespace App.Infra.Db.SqlServer.Ef
{
    public static class RegisterDbContextServices
    {
        public static void AddDbContextServices(this IServiceCollection services, SiteSetting siteSetting)
        {
            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(siteSetting.ConnectionStrings.Sql));


        }
    }
}

