using App.Domain.Core.Contracts.CityAgg.Service;
using App.Domain.Core.Contracts.CustomerAgg.Service;
using App.Domain.Core.Contracts.ExpertAgg.Service;
using App.Domain.Services.CityAgg;
using App.Domain.Services.CustomerAgg;
using App.Domain.Services.ExpertAgg;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Services
{
    public static class RegisterDomainServices
    {
        public static void AddDomainServices(this IServiceCollection services) 
        {
            services.AddScoped<ICityService, CityService>();
            services.AddScoped<ICustomerService, CustomerService>();
            services.AddScoped<IExpertService, ExpertService>();
            services.AddScoped<ICityService, CityService>();
        
        }
    }
}
