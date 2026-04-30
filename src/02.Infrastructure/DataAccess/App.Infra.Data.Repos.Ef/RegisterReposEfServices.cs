using App.Domain.Core.Contracts.CategoryAgg.Repository;
using App.Domain.Core.Contracts.CityAgg.Repository;
using App.Domain.Core.Contracts.CustomerAgg.Repository;
using App.Domain.Core.Contracts.ExpertAgg.Repository;
using App.Domain.Core.Contracts.HomeServiceAgg.Repository;
using App.Infra.Data.Repos.Ef.CategoryAgg;
using App.Infra.Data.Repos.Ef.CityAgg;
using App.Infra.Data.Repos.Ef.CustomerAgg;
using App.Infra.Data.Repos.Ef.ExpertAgg;
using App.Infra.Data.Repos.Ef.HomeServiceAgg;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Infra.Data.Repos.Ef
{
    public static class RegisterReposEfServices
    {

        public static void AddReposEfServices(this IServiceCollection services) 
        {
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<ICityRepository ,CityRepository>();
            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<IExpertRepository, ExpertRepository>();
            services.AddScoped<IHomeServiceRepository, HomeServiceRepository>();

        
        }
    }
}
