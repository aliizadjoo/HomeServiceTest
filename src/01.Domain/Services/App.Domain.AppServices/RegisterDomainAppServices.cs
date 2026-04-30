using App.Domain.AppServices.AccountAgg;
using App.Domain.AppServices.CityAgg;
using App.Domain.Core.Contracts.AccountAgg.AppService;
using App.Domain.Core.Contracts.CityAgg.AppService;
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
        }
    }
}
