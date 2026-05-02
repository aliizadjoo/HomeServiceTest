using App.Domain.AppServices.AccountAgg;
using App.Domain.AppServices.AdminAgg;
using App.Domain.AppServices.CityAgg;
using App.Domain.AppServices.CustomerAgg;
using App.Domain.AppServices.ExpertAgg;
using App.Domain.AppServices.HomeServiceAgg;
using App.Domain.Core.Contracts.AccountAgg.AppService;
using App.Domain.Core.Contracts.AdminAgg.AppService;
using App.Domain.Core.Contracts.CityAgg.AppService;
using App.Domain.Core.Contracts.CustomerAgg.AppService;
using App.Domain.Core.Contracts.ExpertAgg.AppService;
using App.Domain.Core.Contracts.HomeServiceAgg.AppService;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.AppServices
{
    public static class RegisterDomainAppServices
    {
        public static void AddDomainAppServices(this IServiceCollection services) 
        {
            services.AddScoped<ICityAppService , CityAppService>();
            services.AddScoped<IAccountAppService, AccountAppService>();
            services.AddScoped<ICustomerAppService, CustomerAppService>();
            services.AddScoped<IExpertAppService, ExpertAppService>();
            services.AddScoped<IAdminAppService, AdminAppService>();
            services.AddScoped<IHomeServiceAppService, HomeServiceAppService>();
        }
    }
}
