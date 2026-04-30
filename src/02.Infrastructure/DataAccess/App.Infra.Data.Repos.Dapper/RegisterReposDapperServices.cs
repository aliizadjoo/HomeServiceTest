using App.Domain.Core.Contracts.CategoryAgg.Repository;
using App.Domain.Core.Contracts.CityAgg.Repository;
using App.Domain.Core.Contracts.HomeServiceAgg.Repository;
using App.Infra.Data.Repos.Dapper.CategoryAgg;
using App.Infra.Data.Repos.Dapper.CityAgg;
using App.Infra.Data.Repos.Dapper.HomeServiceAgg;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Infra.Data.Repos.Dapper
{
    public static class RegisterReposDapperServices
    {
        public static void AddReposDapperServices(this IServiceCollection services) 
        {
            services.AddScoped<ICategoryRepositoryDapper, CategoryRepositoryDapper>();
            services.AddScoped<IHomeServiceRepositoryDapper, HomeServiceRepositoryDapper>();
            services.AddScoped<ICityRepositoryDapper, CityRepositoryDapper>();
        }
    }
}
