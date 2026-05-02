using App.Domain.Core._common;
using App.Domain.Core.Contracts.HomeServiceAgg.AppService;
using App.Domain.Core.Contracts.HomeServiceAgg.Service;
using App.Domain.Core.Dtos.HomeServiceAgg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.AppServices.HomeServiceAgg
{
    public class HomeServiceAppService(IHomeServiceService _homeServiceService) : IHomeServiceAppService
    {
        public async Task<Result<List<HomeServiceDto>>> GetAll(CancellationToken cancellationToken)
        {
           return await _homeServiceService.GetAll(cancellationToken);
        }
    }
}
