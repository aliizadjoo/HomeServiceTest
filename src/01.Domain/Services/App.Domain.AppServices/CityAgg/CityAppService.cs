using App.Domain.Core.Contracts.CityAgg.AppService;
using App.Domain.Core.Contracts.CityAgg.Service;
using App.Domain.Core.Dtos.CityAgg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.AppServices.CityAgg
{
    public class CityAppService(ICityService _cityService) : ICityAppService
    {
        public Task<List<CityDto>> GeAll(CancellationToken cancellationToken)
        {
            return _cityService.GeAll(cancellationToken);
        }
    }
}
