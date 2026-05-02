using App.Domain.Core._common;
using App.Domain.Core.Contracts.HomeServiceAgg.Repository;
using App.Domain.Core.Contracts.HomeServiceAgg.Service;
using App.Domain.Core.Dtos.HomeServiceAgg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Services.HomeServiceAgg
{
    public class HomeServiceService(IHomeServiceRepositoryDapper _homeServiceRepositoryDapper) : IHomeServiceService
    {
        public async Task<Result<List<HomeServiceDto>>> GetAll(CancellationToken cancellationToken)
        {
            var homeServicesDto =  await _homeServiceRepositoryDapper.GetAll(cancellationToken);

            if (homeServicesDto != null && homeServicesDto.Any())
            {
                return Result<List<HomeServiceDto>>.Success(homeServicesDto);
            }

            return Result<List<HomeServiceDto>>.Failure("سرویسی وجود ندارد.");
        }
    }
}
